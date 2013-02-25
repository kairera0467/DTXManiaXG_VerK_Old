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
	internal class CActFIFOWhiteClear : CActivity
	{
		// メソッド

		public void tフェードアウト開始()
		{
			this.mode = EFIFOモード.フェードアウト;
			this.counter = new CCounter( 0, 400, 5, CDTXMania.Timer );
		}
		public void tフェードイン開始()
		{
			this.mode = EFIFOモード.フェードイン;
			this.counter = new CCounter( 0, 400, 5, CDTXMania.Timer );
		}
		public void tフェードイン完了()		// #25406 2011.6.9 yyagi
		{
			this.counter.n現在の値 = this.counter.n終了値;
		}

		// CActivity 実装
        public override void On活性化(Device D3D9Device)
        {
            if (this.b活性化してる)
                return;

            //this.ds背景動画 = DTXMania.CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(DTXMania.CSkin.Path(@"Graphics\7_StageClear.mp4"), DTXMania.CDTXMania.App.hWnd, true);

            base.On活性化(D3D9Device);
        }
		public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx白タイル64x64 );
                CDTXMania.tテクスチャの解放( ref this.txFullCombo );
                CDTXMania.tテクスチャの解放( ref this.txExcellent );
                CDTXMania.tテクスチャの解放( ref this.tx黒幕 );
                if (this.avi != null)
                {
                    this.avi.Dispose();
                    this.avi = null;
                }
				base.On非活性化();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.tx白タイル64x64 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ), false );
                this.txFullCombo = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_FullCombo.png"));
                this.txExcellent = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Excellent.png"));
                this.tx黒幕 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Drums_black.png"));
                this.txリザルト画像がないときの画像 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenSelect preimage default.png"));
                this.sfリザルトAVI画像 = Surface.CreateOffscreenPlain(CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.SystemMemory);
                this.nAVI再生開始時刻 = -1;
                this.n前回描画したフレーム番号 = -1;
                this.b動画フレームを作成した = false;
                this.pAVIBmp = IntPtr.Zero;

                this.tリザルト動画の指定があれば構築する();
				base.OnManagedリソースの作成();
			}
		}
        
        
        public override void OnManagedリソースの解放()
        {
            if (this.b活性化してない)
                return;
            if (this.sfリザルトAVI画像 != null)
            {
                this.sfリザルトAVI画像.Dispose();
                this.sfリザルトAVI画像 = null;
            }
            CDTXMania.tテクスチャの解放( ref this.r表示するリザルト画像 );
            CDTXMania.tテクスチャの解放( ref this.txリザルト画像がないときの画像 );

            base.OnManagedリソースの解放();
        }

        public override int On進行()
		{
            if (base.b活性化してない || (this.counter == null))
				return 0;

			// 進行。
            this.counter.t進行();
			#region [ 初めての進行処理。]
			//-----------------
			if( this.b初めての進行描画 )
			{
				//if( this.ds背景動画 != null )
				{
					//this.ds背景動画.bループ再生 = false;
					//this.ds背景動画.t再生開始();
				}

				this.b初めての進行描画 = false;
			}
			//-----------------
			#endregion

			//if( this.ds背景動画 != null && !this.ds背景動画.b再生中 )			// 再生完了したらステージ終了。
            if (this.counter.n現在の値 != 400)
				return 0;

			return 1;
		}
		public override void On描画( Device D3D9Device )
		{
			if( this.b活性化してない )
				return;

			#region [ 背景動画 ]
			//-----------------
			//if( this.ds背景動画 != null &&
			//	this.tx背景動画 != null )
			{

				//this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する( this.tx背景動画 );

				//this.tx背景動画.t2D描画( D3D9Device, 0, 0 );
			}
			//-----------------
			#endregion
            if (this.counter.n現在の値 >= 300)
            {
                if (this.tx白タイル64x64 != null)
                {
                    if (this.counter.n現在の値 <= 300)
                    {
                        this.tx白タイル64x64.n透明度 = 0;
                    }
                    else
                    {
                        this.tx白タイル64x64.n透明度 = (this.mode == EFIFOモード.フェードイン) ? (((100 - (this.counter.n現在の値 - 300)) * 0xff) / 100) : (((this.counter.n現在の値 - 300) * 255) / 100);
                    }
                    for (int i = 0; i <= (SampleFramework.GameWindowSize.Width / 64); i++)		// #23510 2010.10.31 yyagi: change "clientSize.Width" to "640" to fix FIFO drawing size
                    {
                        for (int j = 0; j <= (SampleFramework.GameWindowSize.Height / 64); j++)	// #23510 2010.10.31 yyagi: change "clientSize.Height" to "480" to fix FIFO drawing size
                        {
                            this.tx白タイル64x64.t2D描画(CDTXMania.app.Device, i * 64, j * 64);
                        }
                    }
                }
            }
		}

		public override unsafe int On進行描画()
        {
            if (base.b活性化してない || (this.counter == null))
            {
                return 0;
            }
            this.counter.t進行();
            if (!CDTXMania.ConfigIni.bドラムが全部オートプレイである)
            {
                if (CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Poor == 0)
                {
                    if (CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Perfect == CDTXMania.DTX.n可視チップ数.Drums)
                    {
                        this.tx黒幕.t2D描画(CDTXMania.app.Device, 0, 0);
                        this.txExcellent.t2D描画(CDTXMania.app.Device, 0, 0);
                    }
                    else
                    {
                        this.tx黒幕.t2D描画(CDTXMania.app.Device, 0, 0);
                        this.txFullCombo.t2D描画(CDTXMania.app.Device, 0, 0);
                    }
                }
            }
            else
            {
                if (CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Poor == 0)
                {
                        this.tx黒幕.t2D描画(CDTXMania.app.Device, 0, 0);
                        this.txExcellent.t2D描画(CDTXMania.app.Device, 0, 0);
                }
            }
            if (this.counter.n現在の値 >= 300)
            {
                int x = 0;
                int y = 0;
                /*
                this.tx描画用 = new CTexture(CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                    if (((this.avi != null) && (this.tx描画用 != null)) && (this.nAVI再生開始時刻 != -1))
                    {
                        int time = (int)((CDTXMania.Timer.n現在時刻 - this.nAVI再生開始時刻) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                        int frameNoFromTime = this.avi.GetFrameNoFromTime(time);
                        if (frameNoFromTime >= this.avi.GetMaxFrameCount())
                        {
                            this.nAVI再生開始時刻 = CDTXMania.Timer.n現在時刻;
                        }
                        else if ((this.n前回描画したフレーム番号 != frameNoFromTime) && !this.b動画フレームを作成した)
                        {
                            this.pAVIBmp = this.avi.GetFramePtr(frameNoFromTime);
                            this.n前回描画したフレーム番号 = frameNoFromTime;
                            this.b動画フレームを作成した = true;
                        }
                    }
                    if (((this.nAVI再生開始時刻 != -1) && (this.avi != null)) && (this.sfリザルトAVI画像 != null))
                    {
                        if (this.b動画フレームを作成した && (this.pAVIBmp != IntPtr.Zero))
                        {
                            DataRectangle rectangle = this.tx描画用.texture.LockRectangle(0, LockFlags.None);
                            DataStream data = rectangle.Data;
                            int num7 = rectangle.Pitch / this.tx描画用.szテクスチャサイズ.Width;
                            BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = (BitmapUtil.BITMAPINFOHEADER*)this.pAVIBmp.ToPointer();
                            if (pBITMAPINFOHEADER->biBitCount == 24)
                            {
                                switch (num7)
                                {
                                    case 2:
                                        this.avi.tBitmap24ToGraphicsStreamR5G6B5(pBITMAPINFOHEADER, data, this.tx描画用.szテクスチャサイズ.Width, this.tx描画用.szテクスチャサイズ.Height);
                                        break;

                                    case 4:
                                        this.avi.tBitmap24ToGraphicsStreamX8R8G8B8(pBITMAPINFOHEADER, data, this.tx描画用.szテクスチャサイズ.Width, this.tx描画用.szテクスチャサイズ.Height);
                                        break;
                                }
                            }
                            this.tx描画用.texture.UnlockRectangle(0);
                            this.b動画フレームを作成した = false;
                        }
                        {
                            this.tx描画用.t2D描画(CDTXMania.app.Device, 0, 0);
                        }
                    }
                
                if (this.sfリザルトAVI画像 != null)
                {
                    if (((this.avi != null) && (this.sfリザルトAVI画像 != null)) && (this.nAVI再生開始時刻 != -1))
                    {
                        int time = (int)((CDTXMania.Timer.n現在時刻 - this.nAVI再生開始時刻) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                        int frameNoFromTime = this.avi.GetFrameNoFromTime(time);
                        if (frameNoFromTime >= this.avi.GetMaxFrameCount())
                        {
                            this.nAVI再生開始時刻 = CDTXMania.Timer.n現在時刻;
                        }
                        else if ((this.n前回描画したフレーム番号 != frameNoFromTime) && !this.b動画フレームを作成した)
                        {
                            this.b動画フレームを作成した = true;
                            this.n前回描画したフレーム番号 = frameNoFromTime;
                            this.pAVIBmp = this.avi.GetFramePtr(frameNoFromTime);
                        }
                    }
                    if (((this.nAVI再生開始時刻 != -1) && (this.avi != null)) && (this.sfリザルトAVI画像 != null))
                    {
                        if (this.b動画フレームを作成した && (this.pAVIBmp != IntPtr.Zero))
                        {
                            DataRectangle rectangle = this.sfリザルトAVI画像.LockRectangle(LockFlags.None);
                            DataStream data = rectangle.Data;
                            int num7 = rectangle.Pitch / this.sfリザルトAVI画像.Description.Width;
                            BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = (BitmapUtil.BITMAPINFOHEADER*)this.pAVIBmp.ToPointer();
                            if (pBITMAPINFOHEADER->biBitCount == 0x18)
                            {
                                switch (num7)
                                {
                                    case 2:
                                        this.avi.tBitmap24ToGraphicsStreamR5G6B5(pBITMAPINFOHEADER, data, this.sfリザルトAVI画像.Description.Width, this.sfリザルトAVI画像.Description.Height);
                                        break;

                                    case 4:
                                        this.avi.tBitmap24ToGraphicsStreamX8R8G8B8(pBITMAPINFOHEADER, data, this.sfリザルトAVI画像.Description.Width, this.sfリザルトAVI画像.Description.Height);
                                        break;
                                }
                            }
                            this.sfリザルトAVI画像.UnlockRectangle();
                            this.b動画フレームを作成した = false;
                        }
                            using (Surface surface = CDTXMania.app.Device.GetBackBuffer(0, 0))
                            {
                                Rectangle sourceRectangle = new Rectangle(0, 0, this.sfリザルトAVI画像.Description.Width, this.sfリザルトAVI画像.Description.Height);
                                if (y < 0)
                                {
                                    sourceRectangle.Y += -y;
                                    sourceRectangle.Height -= -y;
                                    y = 0;
                                }
                                if (sourceRectangle.Height > 0)
                                {
                                    CDTXMania.app.Device.UpdateSurface(this.sfリザルトAVI画像, sourceRectangle, surface, new Point(x, y));
                                }
                            }
                    }
                }
                
                */
                // Size clientSize = CDTXMania.app.Window.ClientSize;	// #23510 2010.10.31 yyagi: delete as of no one use this any longer.

                //if (this.sfリザルトAVI画像 == null)
                //if(this.b動画フレームを作成した == true)
                {
                    if (this.tx白タイル64x64 != null)
                    {
                        if (this.counter.n現在の値 <= 300)
                        {
                            this.tx白タイル64x64.n透明度 = 0;
                        }
                        else
                        {
                            this.tx白タイル64x64.n透明度 = (this.mode == EFIFOモード.フェードイン) ? (((100 - (this.counter.n現在の値 - 300)) * 0xff) / 100) : (((this.counter.n現在の値 - 300) * 255) / 100);
                        }
                        for (int i = 0; i <= (SampleFramework.GameWindowSize.Width / 64); i++)		// #23510 2010.10.31 yyagi: change "clientSize.Width" to "640" to fix FIFO drawing size
                        {
                            for (int j = 0; j <= (SampleFramework.GameWindowSize.Height / 64); j++)	// #23510 2010.10.31 yyagi: change "clientSize.Height" to "480" to fix FIFO drawing size
                            {
                                this.tx白タイル64x64.t2D描画(CDTXMania.app.Device, i * 64, j * 64);
                            }
                        }
                    }
                }

            }
            if (this.counter.n現在の値 != 400)
            {
                return 0;
            }
            return 1;
        }
        


		// その他

		#region [ private ]
		//-----------------
		public CCounter counter;
		private EFIFOモード mode;
		private CTexture tx白タイル64x64;
        private CTexture txFullCombo;
        private CTexture txExcellent;
        private CTexture tx黒幕;
        private CTexture tx描画用;

        private CAvi avi;
        private bool b動画フレームを作成した;
        private long nAVI再生開始時刻;
        private int n前回描画したフレーム番号;
        private IntPtr pAVIBmp;
        private CTexture r表示するリザルト画像;
        private Surface sfリザルトAVI画像;
        private string strAVIファイル名;

        private CTexture txリザルト画像がないときの画像;

        private unsafe void tサーフェイスをクリアする(Surface sf)
        {
            DataRectangle rectangle = sf.LockRectangle(LockFlags.None);
            DataStream data = rectangle.Data;
            switch ((rectangle.Pitch / sf.Description.Width))
            {
                case 4:
                    {
                        uint* numPtr = (uint*)data.DataPointer.ToPointer();
                        for (int i = 0; i < sf.Description.Height; i++)
                        {
                            for (int j = 0; j < sf.Description.Width; j++)
                            {
                                (numPtr + (i * sf.Description.Width))[j] = 0;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        ushort* numPtr2 = (ushort*)data.DataPointer.ToPointer();
                        for (int k = 0; k < sf.Description.Height; k++)
                        {
                            for (int m = 0; m < sf.Description.Width; m++)
                            {
                                (numPtr2 + (k * sf.Description.Width))[m] = 0;
                            }
                        }
                        break;
                    }
            }
            sf.UnlockRectangle();
        }
        private bool tリザルト動画の指定があれば構築する()
        {
            this.strAVIファイル名 = CSkin.Path(@"Graphics\7_StageClear.avi");
            if (!File.Exists(this.strAVIファイル名))
            {
                Trace.TraceWarning("ファイルが存在しません。({0})", new object[] { this.strAVIファイル名 });
                return false;
            }
            if (this.avi != null)
            {
                this.avi.Dispose();
                this.avi = null;
            }
            try
            {
                this.avi = new CAvi(this.strAVIファイル名);
                this.nAVI再生開始時刻 = CDTXMania.Timer.n現在時刻;
                this.n前回描画したフレーム番号 = -1;
                this.b動画フレームを作成した = false;
                this.tサーフェイスをクリアする(this.sfリザルトAVI画像);
                Trace.TraceInformation("リザルト動画を生成しました。({0})", new object[] { this.strAVIファイル名 });
            }
            catch
            {
                Trace.TraceError("リザルト動画の生成に失敗しました。({0})", new object[] { this.strAVIファイル名 });
                this.avi = null;
                this.nAVI再生開始時刻 = -1;
            }
            return true;
            //-----------------
        #endregion
        }
	}
}
