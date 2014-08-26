using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
            #region[ 難易度数字 ]

            ST文字位置[] st文字位置Array2 = new ST文字位置[11];
            ST文字位置 st文字位置12 = new ST文字位置();
            st文字位置12.ch = '0';
            st文字位置12.pt = new Point(0, 16);
            st文字位置Array2[0] = st文字位置12;
            ST文字位置 st文字位置13 = new ST文字位置();
            st文字位置13.ch = '1';
            st文字位置13.pt = new Point(16, 16);
            st文字位置Array2[1] = st文字位置13;
            ST文字位置 st文字位置14 = new ST文字位置();
            st文字位置14.ch = '2';
            st文字位置14.pt = new Point(32, 16);
            st文字位置Array2[2] = st文字位置14;
            ST文字位置 st文字位置15 = new ST文字位置();
            st文字位置15.ch = '3';
            st文字位置15.pt = new Point(48, 16);
            st文字位置Array2[3] = st文字位置15;
            ST文字位置 st文字位置16 = new ST文字位置();
            st文字位置16.ch = '4';
            st文字位置16.pt = new Point(64, 16);
            st文字位置Array2[4] = st文字位置16;
            ST文字位置 st文字位置17 = new ST文字位置();
            st文字位置17.ch = '5';
            st文字位置17.pt = new Point(80, 16);
            st文字位置Array2[5] = st文字位置17;
            ST文字位置 st文字位置18 = new ST文字位置();
            st文字位置18.ch = '6';
            st文字位置18.pt = new Point(96, 16);
            st文字位置Array2[6] = st文字位置18;
            ST文字位置 st文字位置19 = new ST文字位置();
            st文字位置19.ch = '7';
            st文字位置19.pt = new Point(112, 16);
            st文字位置Array2[7] = st文字位置19;
            ST文字位置 st文字位置20 = new ST文字位置();
            st文字位置20.ch = '8';
            st文字位置20.pt = new Point(128, 16);
            st文字位置Array2[8] = st文字位置20;
            ST文字位置 st文字位置21 = new ST文字位置();
            st文字位置21.ch = '9';
            st文字位置21.pt = new Point(144, 16);
            st文字位置Array2[9] = st文字位置21;
            ST文字位置 st文字位置22 = new ST文字位置();
            st文字位置22.ch = '-';
            st文字位置22.pt = new Point(0, 16);
            st文字位置Array2[10] = st文字位置22;
            this.st小文字位置 = st文字位置Array2;

            //大文字
            ST文字位置[] st文字位置Array3 = new ST文字位置[12];
            ST文字位置 st文字位置23 = new ST文字位置();
            st文字位置23.ch = '0';
            st文字位置23.pt = new Point(0, 0);
            st文字位置Array3[0] = st文字位置23;
            ST文字位置 st文字位置24 = new ST文字位置();
            st文字位置24.ch = '1';
            st文字位置24.pt = new Point(20, 0);
            st文字位置Array3[1] = st文字位置24;
            ST文字位置 st文字位置25 = new ST文字位置();
            st文字位置25.ch = '2';
            st文字位置25.pt = new Point(40, 0);
            st文字位置Array3[2] = st文字位置25;
            ST文字位置 st文字位置26 = new ST文字位置();
            st文字位置26.ch = '3';
            st文字位置26.pt = new Point(60, 0);
            st文字位置Array3[3] = st文字位置26;
            ST文字位置 st文字位置27 = new ST文字位置();
            st文字位置27.ch = '4';
            st文字位置27.pt = new Point(80, 0);
            st文字位置Array3[4] = st文字位置27;
            ST文字位置 st文字位置28 = new ST文字位置();
            st文字位置28.ch = '5';
            st文字位置28.pt = new Point(100, 0);
            st文字位置Array3[5] = st文字位置28;
            ST文字位置 st文字位置29 = new ST文字位置();
            st文字位置29.ch = '6';
            st文字位置29.pt = new Point(120, 0);
            st文字位置Array3[6] = st文字位置29;
            ST文字位置 st文字位置30 = new ST文字位置();
            st文字位置30.ch = '7';
            st文字位置30.pt = new Point(140, 0);
            st文字位置Array3[7] = st文字位置30;
            ST文字位置 st文字位置31 = new ST文字位置();
            st文字位置31.ch = '8';
            st文字位置31.pt = new Point(160, 0);
            st文字位置Array3[8] = st文字位置31;
            ST文字位置 st文字位置32 = new ST文字位置();
            st文字位置32.ch = '9';
            st文字位置32.pt = new Point(180, 0);
            st文字位置Array3[9] = st文字位置32;
            this.st大文字位置 = st文字位置Array3;

            #endregion
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

            this.n本体0X = 0x1d5;
            this.n本体0Y = 0x11b;

            if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
            {
                this.nAlbumWidth = 102;
                this.nAlbumHeight = 102;
            }
            else
            {
                this.nAlbumWidth = 128;
                this.nAlbumHeight = 128;
            }

            #region [ 本体位置 ]

            int n上X = 453;
            int n上Y = 11;

            int n下X = 106;
            int n下Y = 430;

            this.n本体X[0] = 0;
            this.n本体Y[0] = 0;

            this.n本体X[1] = 0;
            this.n本体Y[1] = 0;

            this.n本体X[2] = 0;
            this.n本体Y[2] = 0;

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体X[0] = n上X;
                this.n本体Y[0] = n上Y;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[1] = n下X;
                        this.n本体Y[1] = n下Y;
                    }
                    else
                    {
                        this.n本体X[1] = n上X;
                        this.n本体Y[1] = n上Y;
                    }
                }

                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                    {
                        this.n本体X[2] = n上X;
                        this.n本体Y[2] = n上Y;
                    }
                    else
                    {
                        this.n本体X[2] = n下X;
                        this.n本体Y[2] = n下Y;
                    }
                }

            }
            #endregion

            this.ftSongNameFont = new System.Drawing.Font("Impact", 20f, FontStyle.Regular, GraphicsUnit.Pixel);
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
				this.txリザルト画像がないときの画像 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                this.txLevel = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_LevelNumber.png" ) );
				this.sfリザルトAVI画像 = Surface.CreateOffscreenPlain( CDTXMania.app.Device, 0xcc, 0x10d, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.SystemMemory );
				this.nAVI再生開始時刻 = -1;
				this.n前回描画したフレーム番号 = -1;
				this.b動画フレームを作成した = false;
				this.pAVIBmp = IntPtr.Zero;
				if( CDTXMania.ConfigIni.bストイックモード )
				{
					this.txリザルト画像 = this.txリザルト画像がないときの画像;
				}
				else if( ( ( !this.tリザルト画像の指定があれば構築する() ) && ( !this.tプレビュー画像の指定があれば構築する() ) ) )
				{
					this.txリザルト画像 = this.txリザルト画像がないときの画像;
                }

                this.i中央パネル = Image.FromFile( CSkin.Path( @"Graphics\8_center panel.png" ) );
                this.b中央パネル = new Bitmap(1280, 136);
                
                if( File.Exists( CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png" ) )
                    this.txカスタム曲名テクスチャ = CDTXMania.tテクスチャの生成( CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png" );

                this.bmSongNameLength = new Bitmap(1, 1);
                Bitmap bmpCardName = new Bitmap(1, 1);
                Graphics songname = Graphics.FromImage(this.bmSongNameLength);
                Graphics graphics = Graphics.FromImage(this.bmSongNameLength);
                songname.PageUnit = GraphicsUnit.Pixel;
                graphics.PageUnit = GraphicsUnit.Pixel;

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strSongName = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strSongName = CDTXMania.DTX.TITLE;

                this.nSongNamePixelLength = (int)songname.MeasureString(this.strSongName, this.ftSongNameFont).Width;
                this.bmSongNameLength.Dispose();
                Bitmap image = new Bitmap(this.nSongNamePixelLength, (int)Math.Ceiling((double)this.ftSongNameFont.GetHeight()));
                songname = Graphics.FromImage( image );
                graphics = Graphics.FromImage( b中央パネル );

                graphics.DrawImage( this.i中央パネル, 0, 0, 1280, 136 );
                if( this.txカスタム曲名テクスチャ == null )
                    graphics.DrawString(this.strSongName, this.ftSongNameFont, Brushes.White, 578f, 85f);

                songname.Dispose();
                this.tx中央パネル = new CTexture( CDTXMania.app.Device, this.b中央パネル, CDTXMania.TextureFormat );
                this.txSongName = new CTexture(CDTXMania.app.Device, image, CDTXMania.TextureFormat, false);
                image.Dispose();
                this.ftSongNameFont.Dispose();
                //this.txSongDifficulty = new CTexture(CDTXMania.app.Device, bitmap2, CDTXMania.TextureFormat, false);
                //bitmap2.Dispose();
                Bitmap bitmap3 = new Bitmap(100, 100);
                graphics = Graphics.FromImage(bitmap3);

                //this.txSongLevel = new CTexture(CDTXMania.app.Device, bitmap3, CDTXMania.TextureFormat, false);
                graphics.Dispose();
                bitmap3.Dispose();
                graphics.Dispose();
                bmpCardName.Dispose();
                i中央パネル.Dispose();

                base.OnManagedリソースの作成();
			}
		}
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放( ref this.tx中央パネル );
                CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
                CDTXMania.tテクスチャの解放( ref this.txリザルト画像がないときの画像 );
                if (this.sfリザルトAVI画像 != null)
                {
                    this.sfリザルトAVI画像.Dispose();
                    this.sfリザルトAVI画像 = null;
                }
                CDTXMania.tテクスチャの解放( ref this.txカスタム曲名テクスチャ );
                CDTXMania.tテクスチャの解放( ref this.txSongName );
                CDTXMania.tテクスチャの解放( ref this.r表示するリザルト画像 );
                //CDTXMania.tテクスチャの解放( ref this.txSongLevel );
                    
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
				this.ct登場用 = new CCounter( 0, 1500, 1, CDTXMania.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();

            this.tx中央パネル.t2D描画( CDTXMania.app.Device, 0, 267 );
            if( this.r表示するリザルト画像 != null )
            {
                int width = this.r表示するリザルト画像.szテクスチャサイズ.Width;
                int height = this.r表示するリザルト画像.szテクスチャサイズ.Height;

                /*if( this.ct登場用.n現在の値 < 500 )
                {

                    var mat = Matrix.Identity;

                    //mat *= Matrix.RotationX( 0.1f - ( ( 0.1f / 200.0f ) * this.ct登場用.n現在の値 ) );
                    mat *= Matrix.RotationY( this.ct登場用.n現在の値 < 200 ? ( -0.2f - ( ( -0.2f / 200.0f ) * this.ct登場用.n現在の値 ) ) : 0.0f );
                    //mat *= Matrix.RotationZ( this.ct登場用.n現在の値 < 500 ? ( -0.2f - ( ( -0.2f / 500.0f ) * this.ct登場用.n現在の値 ) ) : 0.0f );
                    mat *= Matrix.Scaling( ((float)this.nAlbumWidth) / ((float)width), ((float)this.nAlbumHeight) / (float)height, 0.0f );

                    mat *= Matrix.Translation(0f, 0f, 0f);

                    this.r表示するリザルト画像.t3D描画( CDTXMania.app.Device, mat );
                }
                else 
                */
                {
                    this.r表示するリザルト画像.vc拡大縮小倍率.X = ((float)this.nAlbumWidth) / ((float)width);
                    this.r表示するリザルト画像.vc拡大縮小倍率.Y = ((float)this.nAlbumHeight) / ((float)height);

                    int nアルバムX = 436;
                    int nアルバムY = 271;

                    if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                    {
                        nアルバムX = 449;
                        nアルバムY = 284;
                    }

                    this.r表示するリザルト画像.t2D描画(CDTXMania.app.Device, nアルバムX, nアルバムY, new Rectangle(0, 0, width, height));
                }
            }
            float num;

            for (int i = 0; i < 3; i++)
            {

                if (this.n本体X[i] != 0)
                {
                    STDGBVALUE<double> n表記するLEVEL = new STDGBVALUE<double>();
                    n表記するLEVEL[i] = CDTXMania.DTX.LEVEL[i] / 10.0;
                    n表記するLEVEL[i] += (CDTXMania.DTX.LEVELDEC[i] != 0 ? CDTXMania.DTX.LEVELDEC[i] / 100.0 : 0);
                    int DTXLevel = CDTXMania.DTX.LEVEL[i];
                    double DTXLevelDeci = (DTXLevel * 10 - CDTXMania.DTX.LEVEL[i]);

                    string strLevel = string.Format("{0:0.00}", n表記するLEVEL[i]);

                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                    {
                        num = ((float)CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル[i]);
                    }
                    else
                    {
                        if (CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル[i] > 100)
                        {
                            num = ((float)CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル[i]) / 100.0f;
                        }
                        else
                        {
                            num = ((float)CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベル[i]) / 10f + (CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベルDec[i] != 0 ? CDTXMania.stage選曲.r確定されたスコア.譜面情報.レベルDec[i] / 100.0f : 0);
                        }
                    }

                    if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                    {
                        //    graphics.DrawString(string.Format("{0:00}", num), this.ftSongDifficultyFont, new SolidBrush(Color.FromArgb(0xba, 0xba, 0xba)), (float)0f, (float)-4f);
                        this.t大文字表示(this.n本体X[i] + 653, this.n本体Y[i] + 11, num.ToString());
                    }
                    else
                    {
                        //    graphics.DrawString(string.Format("{0:0.00}", num), this.ftSongDifficultyFont, new SolidBrush(Color.FromArgb(0xba, 0xba, 0xba)), (float)0f, (float)-4f);
                        this.t大文字表示(this.n本体X[i] + 653, this.n本体Y[i] + 11, string.Format(num.ToString().Substring(0, 1)));
                        this.txLevel.t2D描画(CDTXMania.app.Device, this.n本体X[i] + 673, this.n本体Y[i] + 11, new Rectangle(160, 16, 6, 16));
                        this.t小文字表示(this.n本体X[i] + 680, this.n本体Y[i] + 13, string.Format(string.Format("{0,0:00}", strLevel.Substring(1, 3))));
                    }
                }
            }

            if( File.Exists( CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス + "\\TitleTexture.png" ) )
            {
                if( this.txカスタム曲名テクスチャ != null )
                {
                    this.txカスタム曲名テクスチャ.vc拡大縮小倍率 = new Vector3( 0.75f, 0.75f, 1f );
                    this.txカスタム曲名テクスチャ.t2D描画( CDTXMania.app.Device, 578, 345 );
                }
            }
            else
            {
                if( this.txカスタム曲名テクスチャ == null )
                    this.txSongName.t2D描画(CDTXMania.app.Device, ( this.n本体0X + this.nAlbumWidth ) + 3, this.n本体0Y + 0x3f);
            }
            //this.txSongDifficulty.t2D描画(CDTXMania.app.Device, 0x3ea, 20);

			if( !this.ct登場用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
        }
        private CAvi avi;
        private Bitmap bmSongNameLength;
        private bool b動画フレームを作成した;
        public CCounter ct登場用;
        private int nAlbumHeight;
        private int nAlbumWidth;
        private long nAVI再生開始時刻;
        private int nSongNamePixelLength;
        private int n前回描画したフレーム番号;
        private int n本体0X;
        private int n本体0Y;
        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;
        private IntPtr pAVIBmp;
        private CTexture r表示するリザルト画像;
        private Surface sfリザルトAVI画像;
        private string strAVIファイル名;
        private string strSongName;
        private System.Drawing.Font ftSongNameFont;
        private CTexture txLevel;
        private CTexture txSongName;
        private CTexture txリザルト画像;
        private CTexture txリザルト画像がないときの画像;

        private Bitmap b中央パネル;
        private Image i中央パネル;
        private CTexture tx中央パネル;
        private CTexture txカスタム曲名テクスチャ;

        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;


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
        private void t小文字表示(int x, int y, string str)
        {
            this.t小文字表示(x, y, str, false);
        }
        private void t小文字表示(int x, int y, string str, bool b強調)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st小文字位置.Length; i++)
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 16, 14);
                        if (this.txLevel != null)
                        {
                            this.txLevel.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                if (ch == '.')
                {
                    x += 0;
                }
                else
                {
                    x += 16;
                }
            }
        }
        private void t大文字表示(int x, int y, string str)
        {
            this.t大文字表示(x, y, str, false);
        }
        private void t大文字表示(int x, int y, string str, bool bExtraLarge)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                for (int j = 0; j < this.st大文字位置.Length; j++)
                {
                    if (this.st大文字位置[j].ch == c)
                    {
                        int num;
                        int num2;
                        num = 0;
                        num2 = 0;
                        Rectangle rc画像内の描画領域 = new Rectangle(this.st大文字位置[j].pt.X, this.st大文字位置[j].pt.Y, 20, 16);
                        if (c == '.')
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if (this.txLevel != null)
                        {
                            this.txLevel.t2D描画( CDTXMania.app.Device, x, y, rc画像内の描画領域 );
                        }
                        break;
                    }
                }
                if (c == '.')
                {
                    x += 0;
                }
                else
                {
                    x += 20;
                }
            }
        }
		//-----------------
		#endregion
	}
}
