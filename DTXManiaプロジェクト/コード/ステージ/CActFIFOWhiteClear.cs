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
        public void Start()
        {
            if ((this.txボーナス花火 != null))
            {
                for (int i = 0; i < 240; i++)
                {
                    for (int j = 0; j < 240; j++)
                    {
                        if (!this.st青い星[j].b使用中)
                        {
                            this.st青い星[j].b使用中 = true;
                            int n回転初期値 = CDTXMania.Random.Next(360);
                            double num7 = 2.5 + (((double)CDTXMania.Random.Next(40)) / 100.0);
                            this.st青い星[j].ct進行 = new CCounter(0, 100, 20, CDTXMania.Timer);
                            this.st青い星[j].fX = 600; //X座標

                            this.st青い星[j].fY = 350; //Y座標
                            this.st青い星[j].f加速度X = (float)(num7 * Math.Cos((Math.PI * 2 * n回転初期値) / 360.0));
                            this.st青い星[j].f加速度Y = (float)(num7 * (Math.Sin((Math.PI * 2 * n回転初期値) / 360.0) - 0.2));
                            this.st青い星[j].f加速度の加速度X = 0.995f;
                            this.st青い星[j].f加速度の加速度Y = 0.995f;
                            this.st青い星[j].f重力加速度 = 0.00355f;
                            this.st青い星[j].f半径 = (float)(0.5 + (((double)CDTXMania.Random.Next(30)) / 100.0));
                            break;
                        }
                    }
                }
            }
        }

		// CActivity 実装
        public override void On活性化()
        {
            this.ds背景動画 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(CSkin.Path(@"Graphics\7_StageClear.mp4"), CDTXMania.app.WindowHandle, false);
            if(this.ds背景動画 != null)
                Trace.TraceInformation("DShow動画を生成しました。");
            base.On活性化();
        }
		public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
                C共通.tDisposeする(this.ds背景動画); this.ds背景動画 = null;

                if (this.avi != null)
                {
                    this.avi.Dispose();
                    this.avi = null;
                }
                for (int i = 0; i < 16; i++)
                {
                    this.st青い星[i].ct進行 = null;
                }
				base.On非活性化();
			}
		}

        public override void OnManagedリソースの作成( Device D3D9Device )
		{
			if( this.b活性化してない )
				return;

			if( this.ds背景動画 != null )
			{
				//this.tx背景動画 = CDTXMania.tテクスチャを生成する( this.ds背景動画.n幅px, this.ds背景動画.n高さpx );
                this.tx背景動画 = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );

				if( this.tx背景動画 != null )
				{
					this.tx背景動画.vc拡大縮小倍率 = new Vector3(
						( (float) 1280 / (float) this.ds背景動画.n幅px ),
						( (float) 720 / (float) this.ds背景動画.n高さpx ),
						1.0f );
				}
			}
			else
				this.tx背景動画 = null;

			base.OnManagedリソースの作成( D3D9Device );
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                
				this.tx白タイル64x64 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Tile white 64x64.png" ), false );
                this.txリザルト画像 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\8_background.jpg"), false );
                this.txFullCombo = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_FullCombo.png"));
                this.txExcellent = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Excellent.png"));
                this.tx黒幕 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Drums_black.png"));

                this.txボーナス花火 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\ScreenPlayDrums chip star.png"));
                if (this.txボーナス花火 != null)
                {
                    this.txボーナス花火.b加算合成 = true;
                }
                this.tx描画用 = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
                //this.tx背景動画 = new CTexture(CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                this.nAVI再生開始時刻 = -1;
                this.n前回描画したフレーム番号 = -1;
                this.b動画フレームを作成した = false;
                this.pAVIBmp = IntPtr.Zero;
                this.tリザルト動画の指定があれば構築する();

                for (int i = 0; i < 16; i++)
                {
                    this.st青い星[i] = new ST青い星();
                    this.st青い星[i].b使用中 = false;
                    this.st青い星[i].ct進行 = new CCounter();
                }

				base.OnManagedリソースの作成();
			}
		}
        
        
        public override void OnManagedリソースの解放()
        {
            if (this.b活性化してない)
                return;
            if (this.tx描画用 != null)
            {
                this.tx描画用.Dispose();
                this.tx描画用 = null;
            }
            if (this.avi != null)
            {
                this.avi.Dispose();
                this.avi = null;
            }
            CDTXMania.tテクスチャの解放( ref this.txボーナス花火 );
            CDTXMania.tテクスチャの解放( ref this.tx白タイル64x64 );
            CDTXMania.tテクスチャの解放( ref this.txリザルト画像 );
            CDTXMania.tテクスチャの解放( ref this.txFullCombo );
            CDTXMania.tテクスチャの解放( ref this.txExcellent );
            CDTXMania.tテクスチャの解放( ref this.tx黒幕 );

            base.OnManagedリソースの解放();
        }
        
		public override unsafe int On進行描画()
        {
            if (base.b活性化してない || (this.counter == null))
            {
                return 0;
            }
            this.counter.t進行();

            if (CDTXMania.ConfigIni.bDrums有効 == true)
            {
                if (!CDTXMania.ConfigIni.bドラムが全部オートプレイである)
                {
                    if (CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Poor == 0)
                    {
                        if (CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Perfect == CDTXMania.DTX.n可視チップ数.Drums)
                        {
                            this.tx黒幕.t2D描画(CDTXMania.app.Device, 0, 0);
                            #region[ 粉エフェクト ]
                            for (int i = 0; i < 240; i++)
                            {
                                if (this.st青い星[i].b使用中)
                                {
                                    this.st青い星[i].n前回のValue = this.st青い星[i].ct進行.n現在の値;
                                    this.st青い星[i].ct進行.t進行();
                                    if (this.st青い星[i].ct進行.b終了値に達した)
                                    {
                                        this.st青い星[ i ].b使用中 = false;
                                        this.st青い星[i].ct進行.t停止();
                                    }
                                    for (int n = this.st青い星[i].n前回のValue; n < this.st青い星[i].ct進行.n現在の値; n++)
                                    {
                                        this.st青い星[i].fX += this.st青い星[i].f加速度X;
                                        this.st青い星[i].fY -= this.st青い星[i].f加速度Y;
                                        this.st青い星[i].f加速度X *= this.st青い星[i].f加速度の加速度X;
                                        this.st青い星[i].f加速度Y *= this.st青い星[i].f加速度の加速度Y;
                                        //this.st青い星[i].f加速度Y -= this.st青い星[i].f重力加速度;
                                    }
                                    Matrix mat = Matrix.Identity;

                                    float x = (float)(this.st青い星[i].f半径 * Math.Cos((Math.PI / 2 * this.st青い星[i].ct進行.n現在の値) / 100.0));
                                    mat *= Matrix.Scaling(x, x, 1f);
                                    mat *= Matrix.Translation(this.st青い星[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                                    if (this.txボーナス花火 != null)
                                    {
                                        this.txボーナス花火.t3D描画(CDTXMania.app.Device, mat);
                                    }
                                }
                            }
                            this.Start();
                            #endregion
                            this.txExcellent.t2D描画(CDTXMania.app.Device, 0, 0);
                        }
                        else
                        {
                            this.tx黒幕.t2D描画(CDTXMania.app.Device, 0, 0);
                            #region[ 粉エフェクト ]
                            for (int i = 0; i < 240; i++)
                            {
                                if (this.st青い星[i].b使用中)
                                {
                                    this.st青い星[i].n前回のValue = this.st青い星[i].ct進行.n現在の値;
                                    this.st青い星[i].ct進行.t進行();
                                    if (this.st青い星[i].ct進行.b終了値に達した)
                                    {
                                        this.st青い星[ i ].b使用中 = false;
                                        this.st青い星[i].ct進行.t停止();
                                    }
                                    for (int n = this.st青い星[i].n前回のValue; n < this.st青い星[i].ct進行.n現在の値; n++)
                                    {
                                        this.st青い星[i].fX += this.st青い星[i].f加速度X;
                                        this.st青い星[i].fY -= this.st青い星[i].f加速度Y;
                                        this.st青い星[i].f加速度X *= this.st青い星[i].f加速度の加速度X;
                                        this.st青い星[i].f加速度Y *= this.st青い星[i].f加速度の加速度Y;
                                        //this.st青い星[i].f加速度Y -= this.st青い星[i].f重力加速度;
                                    }
                                    Matrix mat = Matrix.Identity;

                                    float x = (float)(this.st青い星[i].f半径 * Math.Cos((Math.PI / 2 * this.st青い星[i].ct進行.n現在の値) / 100.0));
                                    mat *= Matrix.Scaling(x, x, 1f);
                                    mat *= Matrix.Translation(this.st青い星[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                                    if (this.txボーナス花火 != null)
                                    {
                                        this.txボーナス花火.t3D描画(CDTXMania.app.Device, mat);
                                    }
                                }
                            }
                            this.Start();
                            #endregion
                            this.txFullCombo.t2D描画(CDTXMania.app.Device, 0, 0);
                        }
                    }
                }
                else
                {
                    if (CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Poor == 0)
                    {
                        this.tx黒幕.t2D描画(CDTXMania.app.Device, 0, 0);
                        #region[ 粉エフェクト ]
                        this.Start();
                        for (int i = 0; i < 240; i++)
                        {
                            if (this.st青い星[i].b使用中)
                            {
                                this.st青い星[i].n前回のValue = this.st青い星[i].ct進行.n現在の値;
                                this.st青い星[i].ct進行.t進行();
                                if (this.st青い星[i].ct進行.b終了値に達した)
                                {
                                    this.st青い星[ i ].b使用中 = false;
                                    this.st青い星[i].ct進行.t停止();
                                }
                                for (int n = this.st青い星[i].n前回のValue; n < this.st青い星[i].ct進行.n現在の値; n++)
                                {
                                    this.st青い星[i].fX += this.st青い星[i].f加速度X;
                                    this.st青い星[i].fY -= this.st青い星[i].f加速度Y;
                                    this.st青い星[i].f加速度X *= this.st青い星[i].f加速度の加速度X;
                                    this.st青い星[i].f加速度Y *= this.st青い星[i].f加速度の加速度Y;
                                    //this.st青い星[i].f加速度Y -= this.st青い星[i].f重力加速度;
                                }
                                Matrix mat = Matrix.Identity;

                                float x = (float)(this.st青い星[i].f半径 * Math.Cos((Math.PI / 2 * this.st青い星[i].ct進行.n現在の値) / 100.0));
                                mat *= Matrix.Scaling(x, x, 1f);
                                mat *= Matrix.Translation(this.st青い星[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st青い星[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                                if (this.txボーナス花火 != null)
                                {
                                    this.txボーナス花火.t3D描画(CDTXMania.app.Device, mat);
                                }
                            }

                        }
                        #endregion
                        this.txExcellent.t2D描画(CDTXMania.app.Device, 0, 0);
                    }
                }
            }
                if (this.counter.n現在の値 >= 300)
                {
                    if (this.ds背景動画 != null)
                    {
                        this.ds背景動画.bループ再生 = false;
                        this.ds背景動画.t再生開始();
                    }
                    int x = 0;
                    int y = 0;

                    if (this.ds背景動画 != null)
                    {
                        this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する(this.tx描画用);
                        //if (this.ds背景動画.b上下反転)
                        {
                            this.tx描画用.t2D上下反転描画(CDTXMania.app.Device, 0, 0);
                        }
                        //else
                            //this.tx描画用.t2D描画(CDTXMania.app.Device, 0, 0);
                            if (this.ds背景動画.b再生中 == false)
                            {
                                return 0;
                            }
                    }
                    else if (((this.avi != null) && (this.tx描画用 != null)) && (this.nAVI再生開始時刻 != -1))
                    {
                        int time = (int)((CDTXMania.Timer.n現在時刻 - this.nAVI再生開始時刻) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                        int frameNoFromTime = this.avi.GetFrameNoFromTime(time);
                        //Trace.TraceInformation("n前回描画したフレーム番号:{0}(正の数なら正常)", new object[] { this.n前回描画したフレーム番号 });
                        //Trace.TraceInformation("Timer現在時刻:{0}　nAVI再生開始時刻:{1}　time:{2}", new object[] { CDTXMania.Timer.n現在時刻, nAVI再生開始時刻, time });
                        //Trace.TraceInformation("frameNoFromTime:{0}", new object[] { frameNoFromTime });
                        //Trace.TraceInformation("b動画フレームを作成した:{0}", new object[] { this.b動画フレームを作成した });
                        if (frameNoFromTime >= this.avi.GetMaxFrameCount())
                        {
                            this.nAVI再生開始時刻 = (int)CDTXMania.Timer.n現在時刻 - 150;
                        }
                        else if (CDTXMania.Timer.n現在時刻 <= nAVI再生開始時刻)
                        {
                            this.nAVI再生開始時刻 = (int)CDTXMania.Timer.n現在時刻 - 150;
                        }
                        else if ((this.n前回描画したフレーム番号 != frameNoFromTime) && !this.b動画フレームを作成した)
                        {
                            this.pAVIBmp = this.avi.GetFramePtr(frameNoFromTime);
                            this.n前回描画したフレーム番号 = frameNoFromTime;
                            this.b動画フレームを作成した = true;
                        }
                    }
                    //Trace.TraceInformation("b動画フレームを作成した2:{0}(trueがあれば正常)", new object[] { this.b動画フレームを作成した });
                    if ((this.tx描画用 != null))
                    {
                        if (this.b動画フレームを作成した && (this.pAVIBmp != IntPtr.Zero))
                        {
                            DataRectangle rectangle3 = this.tx描画用.texture.LockRectangle(0, LockFlags.None);
                            DataStream data = rectangle3.Data;
                            int num14 = rectangle3.Pitch / this.tx描画用.szテクスチャサイズ.Width;
                            BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = (BitmapUtil.BITMAPINFOHEADER*)this.pAVIBmp.ToPointer();

                            if (pBITMAPINFOHEADER->biBitCount == 24)
                            {
                                switch (num14)
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
                        else
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
                                        //this.tx白タイル64x64.t2D描画(CDTXMania.app.Device, i * 64, j * 64);
                                    }
                                }
                            }
                        }
                        this.tx描画用.t2D描画(CDTXMania.app.Device, 0, 0);
                        if (this.counter.n現在の値 != 400)
                        {
                            return 0;
                        }
                    }
                    // Size clientSize = CDTXMania.app.Window.ClientSize;	// #23510 2010.10.31 yyagi: delete as of no one use this any longer.

                    if (this.avi == null)
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
                        if (this.counter.n現在の値 != 400)
                        {
                            return 0;
                        }
                    }
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
        private CTexture txボーナス花火;
        private CTexture txリザルト画像;
        protected volatile CDirectShow ds背景動画 = null;
        protected CTexture tx背景動画 = null;

        [StructLayout(LayoutKind.Sequential)]
        private struct ST青い星
        {
            public int nLane;
            public bool b使用中;
            public CCounter ct進行;
            public int n前回のValue;
            public float fX;
            public float fY;
            public float f加速度X;
            public float f加速度Y;
            public float f加速度の加速度X;
            public float f加速度の加速度Y;
            public float f重力加速度;
            public float f半径;
        }
        private ST青い星[] st青い星 = new ST青い星[240];
        private CAvi avi;
        private bool b動画フレームを作成した;
        private long nAVI再生開始時刻;
        private int n前回描画したフレーム番号;
        private IntPtr pAVIBmp;
        private string strAVIファイル名;

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
                Trace.TraceInformation("動画を生成しました。({0})", new object[] { this.strAVIファイル名 });
            }
            catch
            {
                Trace.TraceError("動画の生成に失敗しました。({0})", new object[] { this.strAVIファイル名 });
                this.avi = null;
                this.nAVI再生開始時刻 = -1;
            }
            return true;
            //-----------------
        #endregion
        }
	}
}
