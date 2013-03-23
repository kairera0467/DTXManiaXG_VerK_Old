using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CActResultImage : CActivity
	{
		// コンストラクタ

		public CActResultImage()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct登場用.n現在の値 = this.ct登場用.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
            this.n本体X = 0x1d5;
            this.n本体Y = 0x11b;
            this.nAlbumWidth = 102;
            this.nAlbumHeight = 102;
            this.nAlbumWidthXG3 = 128;
            this.nAlbumHeightXG3 = 128;

            this.ftSongNameFont = new System.Drawing.Font("Impact", 24f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftSongDifficultyFont = new System.Drawing.Font("Impact", 15f, FontStyle.Regular);
            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
            {
                this.ftSongNameFont = new System.Drawing.Font("ＤＦＧ平成ゴシック体W5", 14f, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.E)
            {
                this.ftSongNameFont = new System.Drawing.Font("ＤＦＧ平成ゴシック体W5", 20f, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            
            this.iDrumSpeed = Image.FromFile(CSkin.Path(@"Graphics\7_panel_icons.jpg"));
            base.On活性化();

		}
		public override void On非活性化()
		{
			if( this.ct登場用 != null )
			{
				this.ct登場用 = null;
			}
			if( this.avi != null )
			{
				this.avi.Dispose();
				this.avi = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                Brush steelBlue;
				this.txリザルト画像がないときの画像 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );
				this.sfリザルトAVI画像 = Surface.CreateOffscreenPlain( CDTXMania.app.Device, 0xcc, 0x10d, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.SystemMemory );
				this.nAVI再生開始時刻 = -1;
				this.n前回描画したフレーム番号 = -1;
				this.b動画フレームを作成した = false;
				this.pAVIBmp = IntPtr.Zero;
				if( CDTXMania.ConfigIni.bストイックモード )
				{
					this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
				}
				else if( ( ( !this.tリザルト動画の指定があれば構築する() && !this.tリザルト画像の指定があれば構築する() ) && ( !this.tプレビュー動画の指定があれば構築する() && !this.tプレビュー画像の指定があれば構築する() ) ) && !this.t背景画像があればその一部からリザルト画像を構築する() )
				{
					this.r表示するリザルト画像 = this.txリザルト画像がないときの画像;
				}
        this.bmSongNameLength = new Bitmap(1, 1);
        Graphics graphics = Graphics.FromImage(this.bmSongNameLength);
        graphics.PageUnit = GraphicsUnit.Pixel;
        this.strSongName = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
        this.nSongNamePixelLength = (int) graphics.MeasureString(this.strSongName, this.ftSongNameFont).Width;
        graphics.Dispose();
        this.bmSongNameLength.Dispose();
        Bitmap image = new Bitmap(this.nSongNamePixelLength, (int) Math.Ceiling((double) this.ftSongNameFont.GetHeight()));
        graphics = Graphics.FromImage(image);
        graphics.DrawString(this.strSongName, this.ftSongNameFont, Brushes.White, (float) 0f, (float) 0f);
        graphics.Dispose();
        this.txSongName = new CTexture(CDTXMania.app.Device, image, CDTXMania.TextureFormat, false);
        image.Dispose();
        this.ftSongNameFont.Dispose();
        Bitmap bitmap2 = new Bitmap(0x3a, 0x12);
        graphics = Graphics.FromImage(bitmap2);
        switch (CDTXMania.nSongDifficulty)
        {
            case 0:
                steelBlue = Brushes.GhostWhite;
                break;

            case 1:
            case 6:
                steelBlue = Brushes.Green;
                break;

            case 2:
                steelBlue = Brushes.Aqua;
                break;

            case 3:
            case 7:
                steelBlue = Brushes.Yellow;
                break;

            case 4:
            case 8:
                steelBlue = Brushes.Red;
                break;

            case 5:
            case 9:
                steelBlue = Brushes.Purple;
                break;

            case 10:
            case 11:
                steelBlue = Brushes.Gold;
                break;

            default:
                steelBlue = Brushes.Orange;
                break;
        }
        graphics.FillRectangle(steelBlue, (float)0f, (float)0f, (float)58f, (float)18f);
        graphics.DrawString(CDTXMania.strSongDifficulyName.Substring(0, 3), this.ftSongDifficultyFont, new SolidBrush(Color.FromArgb(1, 1, 1)), (float)10f, (float)-4f);
        graphics.Dispose();
        this.txSongDifficulty = new CTexture(CDTXMania.app.Device, bitmap2, CDTXMania.TextureFormat, false);
        bitmap2.Dispose();
        Bitmap bitmap3 = new Bitmap(100, 100);
        graphics = Graphics.FromImage(bitmap3);
        float num;

        if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
        {
            num = ((float)CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル.Drums);
        }
        else
        {
            if (CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル.Drums > 100)
            {
                num = ((float)CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル.Drums);
            }
            else
            {
                num = ((float)CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル.Drums) / 10f;
            }
        }

        if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
        {
            graphics.DrawString(string.Format("{0:00}", num), this.ftSongDifficultyFont, new SolidBrush(Color.FromArgb(0xba, 0xba, 0xba)), (float)0f, (float)-4f);
        }
        else
        {
            graphics.DrawString(string.Format("{0:0.00}", num), this.ftSongDifficultyFont, new SolidBrush(Color.FromArgb(0xba, 0xba, 0xba)), (float)0f, (float)-4f);
        }
        this.txSongLevel = new CTexture(CDTXMania.app.Device, bitmap3, CDTXMania.TextureFormat, false);
        graphics.Dispose();
        bitmap3.Dispose();
        Bitmap bitmap4 = new Bitmap(0x2a, 0x30);
        graphics = Graphics.FromImage(bitmap4);
        graphics.DrawImage(this.iDrumSpeed, new Rectangle(0, 0, 0x2a, 0x30), new Rectangle(0, CDTXMania.ConfigIni.n譜面スクロール速度.Drums * 0x30, 0x2a, 0x30), GraphicsUnit.Pixel);
        this.txDrumSpeed = new CTexture(CDTXMania.app.Device, bitmap4, CDTXMania.TextureFormat, false);
        graphics.Dispose();
        bitmap4.Dispose();
        base.OnManagedリソースの作成();
			}
		}
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放(ref this.txリザルト画像);
                CDTXMania.tテクスチャの解放(ref this.txリザルト画像がないときの画像);
                if (this.sfリザルトAVI画像 != null)
                {
                    this.sfリザルトAVI画像.Dispose();
                    this.sfリザルトAVI画像 = null;
                }
                    CDTXMania.tテクスチャの解放(ref this.txSongName);
                    CDTXMania.tテクスチャの解放(ref this.r表示するリザルト画像);
                    CDTXMania.tテクスチャの解放(ref this.txSongLevel);
                    CDTXMania.tテクスチャの解放(ref this.txSongDifficulty);
                    CDTXMania.tテクスチャの解放(ref this.txDrumSpeed);
                    base.OnManagedリソースの解放();
            }
        }
		public override unsafe int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ct登場用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();
			if( ( ( this.avi != null ) && ( this.sfリザルトAVI画像 != null ) ) && ( this.nAVI再生開始時刻 != -1 ) )
			{
				int time = (int) ( ( CDTXMania.Timer.n現在時刻 - this.nAVI再生開始時刻 ) * ( ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0 ) );
				int frameNoFromTime = this.avi.GetFrameNoFromTime( time );
				if( frameNoFromTime >= this.avi.GetMaxFrameCount() )
				{
					this.nAVI再生開始時刻 = CDTXMania.Timer.n現在時刻;
				}
				else if( ( this.n前回描画したフレーム番号 != frameNoFromTime ) && !this.b動画フレームを作成した )
				{
					this.b動画フレームを作成した = true;
					this.n前回描画したフレーム番号 = frameNoFromTime;
					this.pAVIBmp = this.avi.GetFramePtr( frameNoFromTime );
				}
			}
			int x = this.n本体X;
			int y = this.n本体Y;
			if( ( ( this.nAVI再生開始時刻 != -1 ) && ( this.avi != null ) ) && ( this.sfリザルトAVI画像 != null ) )
			{
				if( this.b動画フレームを作成した && ( this.pAVIBmp != IntPtr.Zero ) )
				{
					DataRectangle rectangle = this.sfリザルトAVI画像.LockRectangle( LockFlags.None );
					DataStream data = rectangle.Data;
					int num7 = rectangle.Pitch / this.sfリザルトAVI画像.Description.Width;
					BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = (BitmapUtil.BITMAPINFOHEADER*) this.pAVIBmp.ToPointer();
					if( pBITMAPINFOHEADER->biBitCount == 0x18 )
					{
						switch( num7 )
						{
							case 2:
								this.avi.tBitmap24ToGraphicsStreamR5G6B5( pBITMAPINFOHEADER, data, this.sfリザルトAVI画像.Description.Width, this.sfリザルトAVI画像.Description.Height );
								break;

							case 4:
								this.avi.tBitmap24ToGraphicsStreamX8R8G8B8( pBITMAPINFOHEADER, data, this.sfリザルトAVI画像.Description.Width, this.sfリザルトAVI画像.Description.Height );
								break;
						}
					}
					this.sfリザルトAVI画像.UnlockRectangle();
					this.b動画フレームを作成した = false;
				}
				using( Surface surface = CDTXMania.app.Device.GetBackBuffer( 0, 0 ) )
				{
					Rectangle sourceRectangle = new Rectangle( 0, 0, this.sfリザルトAVI画像.Description.Width, this.sfリザルトAVI画像.Description.Height );
					if( y < 0 )
					{
						sourceRectangle.Y += -y;
						sourceRectangle.Height -= -y;
						y = 0;
					}
					if( sourceRectangle.Height > 0 )
					{
						CDTXMania.app.Device.UpdateSurface( this.sfリザルトAVI画像, sourceRectangle, surface, new Point( x, y ) );
					}
					goto Label_042F;
				}
			}
			if( this.r表示するリザルト画像 != null )
			{
                int width = this.r表示するリザルト画像.szテクスチャサイズ.Width;
                int height = this.r表示するリザルト画像.szテクスチャサイズ.Height;
                if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                {
                    this.r表示するリザルト画像.vc拡大縮小倍率.X = ((float)this.nAlbumWidthXG3) / ((float)width);
                    this.r表示するリザルト画像.vc拡大縮小倍率.Y = ((float)this.nAlbumHeightXG3) / ((float)height);
                    this.r表示するリザルト画像.t2D描画(CDTXMania.app.Device, 984, 84, new Rectangle(0, 0, width, height));
                }
                else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.E)
                {
                    this.r表示するリザルト画像.vc拡大縮小倍率.X = 245.0f / ((float)width);
                    this.r表示するリザルト画像.vc拡大縮小倍率.Y = 245.0f / ((float)height);
                    this.r表示するリザルト画像.fZ軸中心回転 = 0.3f;
                    this.r表示するリザルト画像.t2D描画(CDTXMania.app.Device, 960, 350, new Rectangle(0, 0, width, height));
                }
                else
                {
                    this.r表示するリザルト画像.vc拡大縮小倍率.X = ((float)this.nAlbumWidth) / ((float)width);
                    this.r表示するリザルト画像.vc拡大縮小倍率.Y = ((float)this.nAlbumHeight) / ((float)height);
                    this.r表示するリザルト画像.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, 0, width, height));
                }

			}
		Label_042F:
            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
            {
                this.txSongName.t2D描画(CDTXMania.app.Device, (910 + this.nAlbumWidth), 60);
                this.txSongDifficulty.t2D描画(CDTXMania.app.Device, 2000, 2000);
                this.txSongLevel.vc拡大縮小倍率.X = 1.5f;
                this.txSongLevel.t2D描画(CDTXMania.app.Device, 680, 240);
                this.txDrumSpeed.vc拡大縮小倍率.X = 0.7619048f;
                this.txDrumSpeed.vc拡大縮小倍率.Y = 0.6666667f;
                this.txDrumSpeed.t2D描画(CDTXMania.app.Device, 490, 479);
            }
            else if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.E)
            {
                this.txSongName.t2D描画(CDTXMania.app.Device, 856, 630);
                this.txDrumSpeed.vc拡大縮小倍率 = new Vector3(0.76190476190476190476190476190476f, 0.66666666666666666666666666666667f, 1.0f);
                this.txDrumSpeed.t2D描画(CDTXMania.app.Device, 800, 634);
            }
            else
            {
                this.txSongName.t2D描画(CDTXMania.app.Device, (this.n本体X + this.nAlbumWidth) + 3, this.n本体Y + 0x3f);
                this.txSongDifficulty.t2D描画(CDTXMania.app.Device, 0x3ea, 20);
                this.txSongLevel.vc拡大縮小倍率.X = 1.5f;
                this.txSongLevel.t2D描画(CDTXMania.app.Device, 0x447, 20);
                this.txDrumSpeed.vc拡大縮小倍率.X = 0.7619048f;
                this.txDrumSpeed.vc拡大縮小倍率.Y = 0.6666667f;
                this.txDrumSpeed.t2D描画(CDTXMania.app.Device, 0x43c, 60);
            }
			if( !this.ct登場用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
        private CAvi avi;
        private Bitmap bmSongNameLength;
        private bool b動画フレームを作成した;
        private CCounter ct登場用;
        private System.Drawing.Font ftSongDifficultyFont;
        private System.Drawing.Font ftSongNameFont;
        private Image iDrumSpeed;
        private int nAlbumHeight;
        private int nAlbumWidth;
        private int nAlbumHeightXG3;
        private int nAlbumWidthXG3;
        private long nAVI再生開始時刻;
        private int nSongNamePixelLength;
        private int n前回描画したフレーム番号;
        private int n本体X;
        private int n本体Y;
        private IntPtr pAVIBmp;
        private CTexture r表示するリザルト画像;
        private Surface sfリザルトAVI画像;
        private string strAVIファイル名;
        private string strSongName;
        private CTexture txDrumSpeed;
        private CTexture txSongDifficulty;
        private CTexture txSongLevel;
        private CTexture txSongName;
        private CTexture txリザルト画像;
        private CTexture txリザルト画像がないときの画像;


		private bool t背景画像があればその一部からリザルト画像を構築する()
		{
			string bACKGROUND;
			if( ( CDTXMania.ConfigIni.bギタレボモード && ( CDTXMania.DTX.BACKGROUND_GR != null ) ) && ( CDTXMania.DTX.BACKGROUND_GR.Length > 0 ) )
			{
				bACKGROUND = CDTXMania.DTX.BACKGROUND_GR;
			}
			else
			{
				bACKGROUND = CDTXMania.DTX.BACKGROUND;
			}
			if( string.IsNullOrEmpty( bACKGROUND ) )
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			bACKGROUND = CDTXMania.DTX.strフォルダ名 + bACKGROUND;
			Bitmap image = null;
			Bitmap bitmap2 = null;
			Bitmap bitmap3 = null;
			try
			{
				image = new Bitmap( bACKGROUND );
				bitmap2 = new Bitmap(SampleFramework.GameWindowSize.Width, SampleFramework.GameWindowSize.Height);
				Graphics graphics = Graphics.FromImage( bitmap2 );
				int x = 0;
				for (int i = 0; i < SampleFramework.GameWindowSize.Height; i += image.Height)
				{
					for (x = 0; x < SampleFramework.GameWindowSize.Width; x += image.Width)
					{
						graphics.DrawImage( image, x, i, image.Width, image.Height );
					}
				}
				graphics.Dispose();
				bitmap3 = new Bitmap( 0xcc, 0x10d );
				graphics = Graphics.FromImage( bitmap3 );
				graphics.DrawImage( bitmap2, 5, 5, new Rectangle( 0x157, 0x6d, 0xcc, 0x10d ), GraphicsUnit.Pixel );
				graphics.Dispose();
				this.txリザルト画像 = new CTexture( CDTXMania.app.Device, bitmap3, CDTXMania.TextureFormat );
				this.r表示するリザルト画像 = this.txリザルト画像;
			}
			catch
			{
				Trace.TraceError( "背景画像の読み込みに失敗しました。({0})", new object[] { bACKGROUND } );
				this.txリザルト画像 = null;
				return false;
			}
			finally
			{
				if( image != null )
				{
					image.Dispose();
				}
				if( bitmap2 != null )
				{
					bitmap2.Dispose();
				}
				if( bitmap3 != null )
				{
					bitmap3.Dispose();
				}
			}
			return true;
		}
		private unsafe void tサーフェイスをクリアする( Surface sf )
		{
			DataRectangle rectangle = sf.LockRectangle( LockFlags.None );
			DataStream data = rectangle.Data;
			switch( ( rectangle.Pitch / sf.Description.Width ) )
			{
				case 4:
					{
						uint* numPtr = (uint*) data.DataPointer.ToPointer();
						for( int i = 0; i < sf.Description.Height; i++ )
						{
							for( int j = 0; j < sf.Description.Width; j++ )
							{
								( numPtr + ( i * sf.Description.Width ) )[ j ] = 0;
							}
						}
						break;
					}
				case 2:
					{
						ushort* numPtr2 = (ushort*) data.DataPointer.ToPointer();
						for( int k = 0; k < sf.Description.Height; k++ )
						{
							for( int m = 0; m < sf.Description.Width; m++ )
							{
								( numPtr2 + ( k * sf.Description.Width ) )[ m ] = 0;
							}
						}
						break;
					}
			}
			sf.UnlockRectangle();
		}
		private bool tプレビュー画像の指定があれば構築する()
		{
			if( string.IsNullOrEmpty( CDTXMania.DTX.PREIMAGE ) )
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
			if( !File.Exists( path ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
				return false;
			}
			this.txリザルト画像 = CDTXMania.tテクスチャの生成( path );
			this.r表示するリザルト画像 = this.txリザルト画像;
			return ( this.r表示するリザルト画像 != null );
		}
		private bool tプレビュー動画の指定があれば構築する()
		{
			if( !CDTXMania.ConfigIni.bAVI有効 )
			{
				return false;
			}
			if( string.IsNullOrEmpty( CDTXMania.DTX.PREMOVIE ) )
			{
				return false;
			}
			this.strAVIファイル名 = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREMOVIE;
			if( !File.Exists( this.strAVIファイル名 ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { this.strAVIファイル名 } );
				return false;
			}
			if( this.avi != null )
			{
				this.avi.Dispose();
				this.avi = null;
			}
			try
			{
				this.avi = new CAvi( this.strAVIファイル名 );
				this.nAVI再生開始時刻 = CDTXMania.Timer.n現在時刻;
				this.n前回描画したフレーム番号 = -1;
				this.b動画フレームを作成した = false;
				this.tサーフェイスをクリアする( this.sfリザルトAVI画像 );
				Trace.TraceInformation( "動画を生成しました。({0})", new object[] { this.strAVIファイル名 } );
			}
			catch
			{
				Trace.TraceError( "動画の生成に失敗しました。({0})", new object[] { this.strAVIファイル名 } );
				this.avi = null;
				this.nAVI再生開始時刻 = -1;
			}
			return true;
		}
		private bool tリザルト画像の指定があれば構築する()
		{
			int rank = CScoreIni.t総合ランク値を計算して返す( CDTXMania.stage結果.st演奏記録.Drums, CDTXMania.stage結果.st演奏記録.Guitar, CDTXMania.stage結果.st演奏記録.Bass );
			if (rank == 99)	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
			{
				rank = 6;
			}
			if (string.IsNullOrEmpty(CDTXMania.DTX.RESULTIMAGE[rank]))
			{
				return false;
			}
			CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
			this.r表示するリザルト画像 = null;
			string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.RESULTIMAGE[ rank ];
			if( !File.Exists( path ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { path } );
				return false;
			}
			this.txリザルト画像 = CDTXMania.tテクスチャの生成( path );
			this.r表示するリザルト画像 = this.txリザルト画像;
			return ( this.r表示するリザルト画像 != null );
		}
		private bool tリザルト動画の指定があれば構築する()
		{
			if( !CDTXMania.ConfigIni.bAVI有効 )
			{
				return false;
			}
			int rank = CScoreIni.t総合ランク値を計算して返す( CDTXMania.stage結果.st演奏記録.Drums, CDTXMania.stage結果.st演奏記録.Guitar, CDTXMania.stage結果.st演奏記録.Bass );
			if (rank == 99)	// #23534 2010.10.28 yyagi: 演奏チップが0個のときは、rankEと見なす
			{
				rank = 6;
			}

			if( string.IsNullOrEmpty( CDTXMania.DTX.RESULTMOVIE[ rank ] ) )
			{
				return false;
			}
			this.strAVIファイル名 = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.RESULTMOVIE[ rank ];
			if( !File.Exists( this.strAVIファイル名 ) )
			{
				Trace.TraceWarning( "ファイルが存在しません。({0})", new object[] { this.strAVIファイル名 } );
				return false;
			}
			if( this.avi != null )
			{
				this.avi.Dispose();
				this.avi = null;
			}
			try
			{
				this.avi = new CAvi( this.strAVIファイル名 );
				this.nAVI再生開始時刻 = CDTXMania.Timer.n現在時刻;
				this.n前回描画したフレーム番号 = -1;
				this.b動画フレームを作成した = false;
				this.tサーフェイスをクリアする( this.sfリザルトAVI画像 );
				Trace.TraceInformation( "動画を生成しました。({0})", new object[] { this.strAVIファイル名 } );
			}
			catch
			{
				Trace.TraceError( "動画の生成に失敗しました。({0})", new object[] { this.strAVIファイル名 } );
				this.avi = null;
				this.nAVI再生開始時刻 = -1;
			}
			return true;
		}
		//-----------------
		#endregion
	}
}
