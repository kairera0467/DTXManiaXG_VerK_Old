using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
    internal class CAct演奏AVI : CActivity
    {
        // コンストラクタ

        public CAct演奏AVI()
        {
            ST基本位置[] st基本位置Array = new ST基本位置[10];

            //LC
            ST基本位置 st基本位置 = new ST基本位置();
            st基本位置.x = 263;
            st基本位置.y = 0;
            st基本位置.rc = new Rectangle(0, 0, 0x60, 0x60);
            st基本位置Array[0] = st基本位置;

            //HH
            ST基本位置 st基本位置2 = new ST基本位置();
            st基本位置2.x = 336;
            st基本位置2.y = 10;
            st基本位置2.rc = new Rectangle(0x60, 0, 0x60, 0x60);
            st基本位置Array[1] = st基本位置2;

            //SD
            ST基本位置 st基本位置3 = new ST基本位置();
            st基本位置3.x = 446;
            st基本位置3.y = 8;
            st基本位置3.rc = new Rectangle(192, 0, 0x60, 0x60);
            st基本位置Array[2] = st基本位置3;

            //BD

            ST基本位置 st基本位置4 = new ST基本位置();
            st基本位置4.x = 565;
            st基本位置4.y = 0x1b;
            st基本位置4.rc = new Rectangle(0, 96, 0x60, 0x60);
            st基本位置Array[3] = st基本位置4;

            //HT
            ST基本位置 st基本位置5 = new ST基本位置();
            st基本位置5.x = 510;
            st基本位置5.y = -4;
            st基本位置5.rc = new Rectangle(0x60, 0x60, 0x60, 0x60);
            st基本位置Array[4] = st基本位置5;

            //LT
            ST基本位置 st基本位置6 = new ST基本位置();
            st基本位置6.x = 622;
            st基本位置6.y = 4;
            st基本位置6.rc = new Rectangle(192, 96, 0x60, 0x60);
            st基本位置Array[5] = st基本位置6;

            ST基本位置 st基本位置7 = new ST基本位置();
            st基本位置7.x = 672;
            st基本位置7.y = 20;
            st基本位置7.rc = new Rectangle(0, 0xc0, 0x60, 0x60);
            st基本位置Array[6] = st基本位置7;

            ST基本位置 st基本位置8 = new ST基本位置();
            st基本位置8.x = 0x2df;
            st基本位置8.y = 0;
            st基本位置8.rc = new Rectangle(0x60, 0xc0, 0x60, 0x60);
            st基本位置Array[7] = st基本位置8;
            ST基本位置 st基本位置9 = new ST基本位置();
            st基本位置9.x = 0x317;
            st基本位置9.y = 8;
            st基本位置9.rc = new Rectangle(192, 0xc0, 0x60, 0x60);
            st基本位置Array[8] = st基本位置9;
            ST基本位置 st基本位置10 = new ST基本位置();
            st基本位置10.x = 0x18c;
            st基本位置10.y = 0x1b;
            st基本位置10.rc = new Rectangle(288, 0x60, 0x60, 0x60);
            st基本位置Array[9] = st基本位置10;
            this.st基本位置 = st基本位置Array;

            base.b活性化してない = true;

        }


        // メソッド

        public void Start(int nチャンネル番号, CDTX.CAVI rAVI, int n開始サイズW, int n開始サイズH, int n終了サイズW, int n終了サイズH, int n画像側開始位置X, int n画像側開始位置Y, int n画像側終了位置X, int n画像側終了位置Y, int n表示側開始位置X, int n表示側開始位置Y, int n表示側終了位置X, int n表示側終了位置Y, int n総移動時間ms, int n移動開始時刻ms)
        {

            if (nチャンネル番号 == 0x54 && CDTXMania.ConfigIni.bAVI有効)
            {
                this.rAVI = rAVI;
                this.n開始サイズW = n開始サイズW;
                this.n開始サイズH = n開始サイズH;
                this.n終了サイズW = n終了サイズW;
                this.n終了サイズH = n終了サイズH;
                this.n画像側開始位置X = n画像側開始位置X;
                this.n画像側開始位置Y = n画像側開始位置Y;
                this.n画像側終了位置X = n画像側終了位置X;
                this.n画像側終了位置Y = n画像側終了位置Y;
                this.n表示側開始位置X = n表示側開始位置X;
                this.n表示側開始位置Y = n表示側開始位置Y;
                this.n表示側終了位置X = n表示側終了位置X;
                this.n表示側終了位置Y = n表示側終了位置Y;
                this.n総移動時間ms = n総移動時間ms;
                this.n移動開始時刻ms = (n移動開始時刻ms != -1) ? n移動開始時刻ms : CDTXMania.Timer.n現在時刻;
                this.n前回表示したフレーム番号 = -1;
                if ((this.rAVI != null) && (this.rAVI.avi != null))
                {
                    float num2;
                    float num3;
                    this.framewidth = this.rAVI.avi.nフレーム幅;
                    this.frameheight = this.rAVI.avi.nフレーム高さ;
                    if (this.tx描画用 == null)
                    {
                        this.tx描画用 = new CTexture(CDTXMania.app.Device, (int)this.framewidth, (int)this.frameheight, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                    }
                    if ((((float)this.framewidth) / ((float)this.frameheight)) < 1.77f)
                    {
                        //旧企画クリップだった場合
                        this.ratio1 = 720f / ((float)this.frameheight);
                        this.position = (int)((1280f - (this.framewidth * this.ratio1)) / 2f);
                        int num = (int)(this.framewidth * this.ratio1);
                        if (num <= 565)
                        {
                            this.position = 295 + ((int)((565f - (this.framewidth * this.ratio1)) / 2f));
                            this.i1 = 0;
                            this.i2 = (int)this.framewidth;
                            this.rec = new Rectangle(0, 0, 0, 0);
                            this.rec3 = new Rectangle(0, 0, 0, 0);
                            this.rec2 = new Rectangle(0, 0, (int)this.framewidth, (int)this.frameheight);
                        }
                        else
                        {
                            this.position = 295 - ((int)(((this.framewidth * this.ratio1) - 565f) / 2f));
                            this.i1 = (int)(((float)(295 - this.position)) / this.ratio1);
                            this.i2 = (int)((565f / ((float)num)) * this.framewidth);
                            this.rec = new Rectangle(0, 0, this.i1, (int)this.frameheight);
                            this.rec3 = new Rectangle(this.i1 + this.i2, 0, (((int)this.framewidth) - this.i1) - this.i2, (int)this.frameheight);
                            this.rec2 = new Rectangle(this.i1, 0, this.i2, (int)this.frameheight);
                        }
                        this.tx描画用.vc拡大縮小倍率.X = this.ratio1;
                        this.tx描画用.vc拡大縮小倍率.Y = this.ratio1;
                    }
                    else
                    {
                        //ワイドクリップの処理
                        this.ratio1 = 1280f / ((float)this.framewidth);
                        this.position = (int)((720f - (this.frameheight * this.ratio1)) / 2f);
                        this.i1 = (int)(this.framewidth * 0.23046875);
                        this.i2 = (int)(this.framewidth * 0.44140625);
                        this.rec = new Rectangle(0, 0, this.i1, (int)this.frameheight);
                        this.rec2 = new Rectangle(this.i1, 0, this.i2, (int)this.frameheight);
                        this.rec3 = new Rectangle(this.i1 + this.i2, 0, (((int)this.framewidth) - this.i1) - this.i2, (int)this.frameheight);
                        this.tx描画用.vc拡大縮小倍率.X = this.ratio1;
                        this.tx描画用.vc拡大縮小倍率.Y = this.ratio1;
                    }


                    if (this.framewidth > 420)
                    {
                        num2 = 420f / ((float)this.framewidth);
                    }
                    else
                    {
                        num2 = 1f;
                    }
                    if (this.frameheight > 580)
                    {
                        num3 = 580f / ((float)this.frameheight);
                    }
                    else
                    {
                        num3 = 1f;
                    }
                    if (num2 > num3)
                    {
                        num2 = num3;
                    }
                    else
                    {
                        num3 = num2;
                    }
                    this.smallvc = new Vector3(num2, num3, 1f);
                    this.vclip = new Vector3(1.42f, 1.42f, 1f);
                }
            }
            if (nチャンネル番号 == 0x99 && CDTXMania.ConfigIni.bAVI有効)
            {
                this.rAVI = rAVI;
                this.n開始サイズW = n開始サイズW;
                this.n開始サイズH = n開始サイズH;
                this.n終了サイズW = n終了サイズW;
                this.n終了サイズH = n終了サイズH;
                this.n画像側開始位置X = n画像側開始位置X;
                this.n画像側開始位置Y = n画像側開始位置Y;
                this.n画像側終了位置X = n画像側終了位置X;
                this.n画像側終了位置Y = n画像側終了位置Y;
                this.n表示側開始位置X = n表示側開始位置X;
                this.n表示側開始位置Y = n表示側開始位置Y;
                this.n表示側終了位置X = n表示側終了位置X;
                this.n表示側終了位置Y = n表示側終了位置Y;
                this.n総移動時間ms = n総移動時間ms;
                this.n移動開始時刻ms = (n移動開始時刻ms != -1) ? n移動開始時刻ms : CDTXMania.Timer.n現在時刻;
                this.n前回表示したフレーム番号 = -1;
                if ((this.rAVI != null) && (this.rAVI.avi != null))
                {
                    float num2;
                    float num3;
                    this.framewidth = this.rAVI.avi.nフレーム幅;
                    this.frameheight = this.rAVI.avi.nフレーム高さ;
                    if (this.tx描画用2 == null)
                    {
                        this.tx描画用2 = new CTexture(CDTXMania.app.Device, (int)this.framewidth, (int)this.frameheight, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                    }
                    if ((((float)this.framewidth) / ((float)this.frameheight)) < 1.77f)
                    {
                        //旧企画クリップだった場合
                        this.ratio1 = 720f / ((float)this.frameheight);
                        this.position = (int)((1280f - (this.framewidth * this.ratio1)) / 2f);
                        int num = (int)(this.framewidth * this.ratio1);
                        if (num <= 565)
                        {
                            this.position = 295 + ((int)((565f - (this.framewidth * this.ratio1)) / 2f));
                            this.i1 = 0;
                            this.i2 = (int)this.framewidth;
                            this.rec = new Rectangle(0, 0, 0, 0);
                            this.rec3 = new Rectangle(0, 0, 0, 0);
                            this.rec2 = new Rectangle(0, 0, (int)this.framewidth, (int)this.frameheight);
                        }
                        else
                        {
                            this.position = 295 - ((int)(((this.framewidth * this.ratio1) - 565f) / 2f));
                            this.i1 = (int)(((float)(295 - this.position)) / this.ratio1);
                            this.i2 = (int)((565f / ((float)num)) * this.framewidth);
                            this.rec = new Rectangle(0, 0, this.i1, (int)this.frameheight);
                            this.rec3 = new Rectangle(this.i1 + this.i2, 0, (((int)this.framewidth) - this.i1) - this.i2, (int)this.frameheight);
                            this.rec2 = new Rectangle(this.i1, 0, this.i2, (int)this.frameheight);
                        }
                        this.tx描画用2.vc拡大縮小倍率.X = this.ratio1;
                        this.tx描画用2.vc拡大縮小倍率.Y = this.ratio1;
                    }
                    else
                    {
                        //ワイドクリップの処理
                        this.ratio1 = 1280f / ((float)this.framewidth);
                        this.position = (int)((720f - (this.frameheight * this.ratio1)) / 2f);
                        this.i1 = (int)(this.framewidth * 0.23046875);
                        this.i2 = (int)(this.framewidth * 0.44140625);
                        this.rec = new Rectangle(0, 0, this.i1, (int)this.frameheight);
                        this.rec2 = new Rectangle(this.i1, 0, this.i2, (int)this.frameheight);
                        this.rec3 = new Rectangle(this.i1 + this.i2, 0, (((int)this.framewidth) - this.i1) - this.i2, (int)this.frameheight);
                        this.tx描画用2.vc拡大縮小倍率.X = this.ratio1;
                        this.tx描画用2.vc拡大縮小倍率.Y = this.ratio1;
                    }


                    if (this.framewidth > 420)
                    {
                        num2 = 420f / ((float)this.framewidth);
                    }
                    else
                    {
                        num2 = 1f;
                    }
                    if (this.frameheight > 580)
                    {
                        num3 = 580f / ((float)this.frameheight);
                    }
                    else
                    {
                        num3 = 1f;
                    }
                    if (num2 > num3)
                    {
                        num2 = num3;
                    }
                    else
                    {
                        num3 = num2;
                    }
                    this.vclip = new Vector3(1.42f, 1.42f, 1f);
                    this.smallvc = new Vector3(num2, num3, 1f);
                }
            }
        }
        public void SkipStart(int n移動開始時刻ms)
        {
            foreach (CDTX.CChip chip in CDTXMania.DTX.listChip)
            {
                if (chip.n発声時刻ms > n移動開始時刻ms)
                {
                    break;
                }
                switch (chip.eAVI種別)
                {
                    case EAVI種別.AVI:
                        {
                            if (chip.rAVI != null)
                            {
                                this.Start(chip.nチャンネル番号, chip.rAVI, 1280, 720, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chip.n発声時刻ms);
                            }
                            continue;
                        }
                    case EAVI種別.AVIPAN:
                        {
                            if (chip.rAVIPan != null)
                            {
                                this.Start(chip.nチャンネル番号, chip.rAVI, chip.rAVIPan.sz開始サイズ.Width, chip.rAVIPan.sz開始サイズ.Height, chip.rAVIPan.sz終了サイズ.Width, chip.rAVIPan.sz終了サイズ.Height, chip.rAVIPan.pt動画側開始位置.X, chip.rAVIPan.pt動画側開始位置.Y, chip.rAVIPan.pt動画側終了位置.X, chip.rAVIPan.pt動画側終了位置.Y, chip.rAVIPan.pt表示側開始位置.X, chip.rAVIPan.pt表示側開始位置.Y, chip.rAVIPan.pt表示側終了位置.X, chip.rAVIPan.pt表示側終了位置.Y, chip.n総移動時間, chip.n発声時刻ms);
                            }
                            continue;
                        }
                }
            }
        }
        public void Stop()
        {
            if ((this.rAVI != null) && (this.rAVI.avi != null))
            {
                this.n移動開始時刻ms = -1;
            }
        }
        public void MovieMode()
        {
            this.nCurrentMovieMode = CDTXMania.ConfigIni.nMovieMode;
            if ((this.nCurrentMovieMode == 1) || (this.nCurrentMovieMode == 3))
            {
                this.bFullScreen = true;
            }
            else
            {
                this.bFullScreen = false;
            }
            if ((this.nCurrentMovieMode == 2) || (this.nCurrentMovieMode == 3))
            {
                this.bWindowMode = true;
            }
            else
            {
                this.bWindowMode = false;
            }
        }

        public void Cont(int n再開時刻ms)
        {
            if ((this.rAVI != null) && (this.rAVI.avi != null))
            {
                this.n移動開始時刻ms = n再開時刻ms;
            }
        }


        // CActivity 実装
        public override void On活性化()
        {
            this.rAVI = null;
            this.n移動開始時刻ms = -1;
            this.n前回表示したフレーム番号 = -1;
            this.bフレームを作成した = false;
            this.pBmp = IntPtr.Zero;
            this.MovieMode();
            this.nAlpha = 249 - ((int)(((float)(CDTXMania.ConfigIni.nMovieAlpha * 249)) / 10f));
            if (CDTXMania.ConfigIni.nMovieAlpha == 11)
            {
                nAlpha = 255;
            }
            this.ct右シンバル = new CCounter(0, 8, 35, CDTXMania.Timer);
            this.ct左シンバル = new CCounter(0, 8, 35, CDTXMania.Timer);

            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txドラム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums.png"));
                this.tx左シンバル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_LCymbal.png"));
                this.tx右シンバル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_RCymbal.png"));
                this.txスネア = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_Snare.png"));
                this.txハイタム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_HiTom.png"));
                this.txロータム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_LowTom.png"));
                this.txフロアタム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_FloorTom.png"));
                this.txバスドラ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums_BassDrum.png"));
                this.txバートップ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_BarTops.png"));
                if (CDTXMania.ConfigIni.bGraph.Drums == true)
                {
                    this.txクリップパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_ClipPanelB.png"));
                }
                else
                {
                    this.txクリップパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_ClipPanel.png"));
                }
                
                this.txLivePoint = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_LivePointGauge.png"));
                this.txScore = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_score numbers.png"));
                string pathBPMFL = CSkin.Path(@"Graphics\7_BPMbar_Flush_L.png");
                string pathBPMFR = CSkin.Path(@"Graphics\7_BPMbar_Flush_R.png");
                string pathBPMbarL = CSkin.Path(@"Graphics\7_BPMbarL.png");
                string pathBPMbarR = CSkin.Path(@"Graphics\7_BPMbarR.png");
                if(File.Exists(pathBPMbarL) && File.Exists(pathBPMbarR))
                {
                    this.txBPMバー左 = CDTXMania.tテクスチャの生成(pathBPMbarL);
                    this.txBPMバー右 = CDTXMania.tテクスチャの生成(pathBPMbarR);
                }
                if (File.Exists(pathBPMFL) && File.Exists(pathBPMFR))
                {
                    this.txBPMバーフラッシュ左 = CDTXMania.tテクスチャの生成(pathBPMFL);
                    this.txBPMバーフラッシュ右 = CDTXMania.tテクスチャの生成(pathBPMFR);
                }

                for (int i = 0; i < 2; i++)
                {
                    this.stフィルイン[i] = new STフィルイン();
                    this.stフィルイン[i].ct進行 = new CCounter(0, 31, 30, CDTXMania.Timer);
                    this.stフィルイン[i].b使用中 = false;
                }
                this.txフィルインエフェクト = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Fillin Effect.png"));

                //レーン周りの処理。つーか何でbitmapなんだ?
                this.blanes = new Bitmap(558, 720);
                if (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true)
                {
                    this.blanes = new Bitmap(1280, 720);
                }
                Graphics graphics = Graphics.FromImage(this.blanes);
                if (CDTXMania.ConfigIni.bDrums有効)
                {
                    #region[ レーン ]
                    if (CDTXMania.ConfigIni.eDark != Eダークモード.OFF)
                    {
                        this.ilanes = Image.FromFile(CSkin.Path(@"Graphics\7_lanes_dark.png"));
                    }
                    #endregion
                }
                else if (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true)
                {
                    this.ilanes = Image.FromFile(CSkin.Path(@"Graphics\7_lanes_Guitar.png"));
                }

                if (CDTXMania.ConfigIni.bDrums有効 && CDTXMania.ConfigIni.eDark != Eダークモード.OFF)
                {
                    graphics.DrawImage(this.ilanes, new Rectangle(0, 0, 558, 720), new Rectangle(0, 0, 558, 720), GraphicsUnit.Pixel);
                }
                else if (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true)
                {
                    graphics.DrawImage(this.ilanes, new Rectangle(0, 0, 1280, 720), new Rectangle(0, 0, 1280, 720), GraphicsUnit.Pixel);
                }

                if (CDTXMania.ConfigIni.eDark != Eダークモード.OFF || (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true))
                {
                    graphics.Dispose();
                    this.ilanes.Dispose();
                    this.txlanes = new CTexture(CDTXMania.app.Device, this.blanes, CDTXMania.TextureFormat, false);
                    this.blanes.Dispose();
                }
                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                //特殊テクスチャ 3枚
                if (this.tx描画用 != null)
                {
                    this.tx描画用.Dispose();
                    if (this.tx描画用2 != null)
                        this.tx描画用2.Dispose();
                    this.tx描画用 = null;
                }
                if (this.txlanes != null)
                    this.txlanes.Dispose();
                //テクスチャ 17枚
                CDTXMania.tテクスチャの解放(ref this.txScore);
                CDTXMania.tテクスチャの解放(ref this.txLivePoint);
                CDTXMania.tテクスチャの解放(ref this.txドラム);
                CDTXMania.tテクスチャの解放(ref this.tx左シンバル);
                CDTXMania.tテクスチャの解放(ref this.tx右シンバル);
                CDTXMania.tテクスチャの解放(ref this.txBPMバー左);
                CDTXMania.tテクスチャの解放(ref this.txBPMバー右);
                CDTXMania.tテクスチャの解放(ref this.txスネア);
                CDTXMania.tテクスチャの解放(ref this.txハイタム);
                CDTXMania.tテクスチャの解放(ref this.txロータム);
                CDTXMania.tテクスチャの解放(ref this.txフロアタム);
                CDTXMania.tテクスチャの解放(ref this.txバスドラ);
                CDTXMania.tテクスチャの解放(ref this.txバートップ);
                CDTXMania.tテクスチャの解放(ref this.txクリップパネル);
                CDTXMania.tテクスチャの解放(ref this.txフィルインエフェクト);
                CDTXMania.tテクスチャの解放(ref this.txBPMバーフラッシュ左);
                CDTXMania.tテクスチャの解放(ref this.txBPMバーフラッシュ右);
                base.OnManagedリソースの解放();
            }
        }
        public unsafe int t進行描画(int x, int y)
        {
            int RCym = this.ct右シンバル.n現在の値;
            int LCym = this.ct左シンバル.n現在の値;
            int num1 = 0;
            if (CDTXMania.ConfigIni.bDrums有効 == true)
            {
                num1 = CDTXMania.stage演奏ドラム画面.ctBPMバー.n現在の値;
            }
            else if (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true)
            {
                num1 = CDTXMania.stage演奏ギター画面.ctBPMバー.n現在の値;
            }

            #region[ムービーのフレーム作成処理]
            if ((!base.b活性化してない))
            {
                if (((this.bFullScreen || this.bWindowMode) && this.tx描画用 != null))
                {
                    int time = (int)((CDTXMania.Timer.n現在時刻 - this.n移動開始時刻ms) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                    int frameNoFromTime = this.rAVI.avi.GetFrameNoFromTime(time);
                    if ((this.n総移動時間ms != 0) && (this.n総移動時間ms < time))
                    {
                        this.n総移動時間ms = 0;
                        this.n移動開始時刻ms = -1L;
                    }
                    else if ((this.n総移動時間ms == 0) && (frameNoFromTime >= this.rAVI.avi.GetMaxFrameCount()))
                    {
                        this.n移動開始時刻ms = -1L;
                    }
                    else
                    {
                        Rectangle rectangle;
                        Rectangle rectangle2;
                        if (!((this.n前回表示したフレーム番号 == frameNoFromTime) || this.bフレームを作成した))
                        {
                            this.pBmp = this.rAVI.avi.GetFramePtr(frameNoFromTime);
                            this.n前回表示したフレーム番号 = frameNoFromTime;
                            this.bフレームを作成した = true;
                        }
                        Size size = new Size((int)this.rAVI.avi.nフレーム幅, (int)this.rAVI.avi.nフレーム高さ);
                        Size size2 = new Size(1280, 720);
                        Size size3 = new Size(this.n開始サイズW, this.n開始サイズH);
                        Size size4 = new Size(this.n終了サイズW, this.n終了サイズH);
                        Point location = new Point(this.n画像側開始位置X, this.n画像側終了位置Y);
                        Point point2 = new Point(this.n画像側終了位置X, this.n画像側終了位置Y);
                        Point point3 = new Point(this.n表示側開始位置X, this.n表示側開始位置Y);
                        Point point4 = new Point(this.n表示側終了位置X, this.n表示側終了位置Y);
                        if (CDTXMania.Timer.n現在時刻 < this.n移動開始時刻ms)
                        {
                            this.n移動開始時刻ms = CDTXMania.Timer.n現在時刻;
                        }
                        time = (int)((CDTXMania.Timer.n現在時刻 - this.n移動開始時刻ms) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                        if (this.n総移動時間ms == 0)
                        {
                            rectangle = new Rectangle(location, size3);
                            rectangle2 = new Rectangle(point3, size3);
                        }
                        else
                        {
                            double num5 = ((double)time) / ((double)this.n総移動時間ms);
                            Size size5 = new Size(size3.Width + ((int)((size4.Width - size3.Width) * num5)), size3.Height + ((int)((size4.Height - size3.Height) * num5)));
                            rectangle = new Rectangle((int)((point2.X - location.X) * num5), (int)((point2.Y - location.Y) * num5), ((int)((point2.X - location.X) * num5)) + size5.Width, ((int)((point2.Y - location.Y) * num5)) + size5.Height);
                            rectangle2 = new Rectangle((int)((point4.X - point3.X) * num5), (int)((point4.Y - point3.Y) * num5), ((int)((point4.X - point3.X) * num5)) + size5.Width, ((int)((point4.Y - point3.Y) * num5)) + size5.Height);
                            if ((((rectangle.Right <= 0) || (rectangle.Bottom <= 0)) || ((rectangle.Left >= size.Width) || (rectangle.Top >= size.Height))) || (((rectangle2.Right <= 0) || (rectangle2.Bottom <= 0)) || ((rectangle2.Left >= size2.Width) || (rectangle2.Top >= size2.Height))))
                            {
                                goto Label_0A06;
                            }
                            if (rectangle.X < 0)
                            {
                                int num6 = -rectangle.X;
                                rectangle2.X += num6;
                                rectangle2.Width -= num6;
                                rectangle.X = 0;
                                rectangle.Width -= num6;
                            }
                            if (rectangle.Y < 0)
                            {
                                int num7 = -rectangle.Y;
                                rectangle2.Y += num7;
                                rectangle2.Height -= num7;
                                rectangle.Y = 0;
                                rectangle.Height -= num7;
                            }
                            if (rectangle.Right > size.Width)
                            {
                                int num8 = rectangle.Right - size.Width;
                                rectangle2.Width -= num8;
                                rectangle.Width -= num8;
                            }
                            if (rectangle.Bottom > size.Height)
                            {
                                int num9 = rectangle.Bottom - size.Height;
                                rectangle2.Height -= num9;
                                rectangle.Height -= num9;
                            }
                            if (rectangle2.X < 0)
                            {
                                int num10 = -rectangle2.X;
                                rectangle.X += num10;
                                rectangle.Width -= num10;
                                rectangle2.X = 0;
                                rectangle2.Width -= num10;
                            }
                            if (rectangle2.Y < 0)
                            {
                                int num11 = -rectangle2.Y;
                                rectangle.Y += num11;
                                rectangle.Height -= num11;
                                rectangle2.Y = 0;
                                rectangle2.Height -= num11;
                            }
                            if (rectangle2.Right > size2.Width)
                            {
                                int num12 = rectangle2.Right - size2.Width;
                                rectangle.Width -= num12;
                                rectangle2.Width -= num12;
                            }
                            if (rectangle2.Bottom > size2.Height)
                            {
                                int num13 = rectangle2.Bottom - size2.Height;
                                rectangle.Height -= num13;
                                rectangle2.Height -= num13;
                            }
                            if ((((rectangle.X >= rectangle.Right) || (rectangle.Y >= rectangle.Bottom)) || ((rectangle2.X >= rectangle2.Right) || (rectangle2.Y >= rectangle2.Bottom))) || ((((rectangle.Right < 0) || (rectangle.Bottom < 0)) || ((rectangle.X > size.Width) || (rectangle.Y > size.Height))) || (((rectangle2.Right < 0) || (rectangle2.Bottom < 0)) || ((rectangle2.X > size2.Width) || (rectangle2.Y > size2.Height)))))
                            {
                                goto Label_0A06;
                            }
                        }
                        
                        if ((this.tx描画用 != null) && (this.n総移動時間ms != -1))
                        {
                            if (this.bフレームを作成した && (this.pBmp != IntPtr.Zero))
                            {
                                DataRectangle rectangle3 = this.tx描画用.texture.LockRectangle(0, LockFlags.None);
                                DataStream data = rectangle3.Data;
                                int num14 = rectangle3.Pitch / this.tx描画用.szテクスチャサイズ.Width;
                                BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = (BitmapUtil.BITMAPINFOHEADER*)this.pBmp.ToPointer();
                                if (pBITMAPINFOHEADER->biBitCount == 0x18)
                                {
                                    switch (num14)
                                    {
                                        case 2:
                                            this.rAVI.avi.tBitmap24ToGraphicsStreamR5G6B5(pBITMAPINFOHEADER, data, this.tx描画用.szテクスチャサイズ.Width, this.tx描画用.szテクスチャサイズ.Height);
                                            break;

                                        case 4:
                                            this.rAVI.avi.tBitmap24ToGraphicsStreamX8R8G8B8(pBITMAPINFOHEADER, data, this.tx描画用.szテクスチャサイズ.Width, this.tx描画用.szテクスチャサイズ.Height);
                                            break;
                                    }
                                }
                                this.tx描画用.texture.UnlockRectangle(0);
                                this.bフレームを作成した = false;
                            }
                            
                            if (this.bFullScreen)
                            {
                                if (((float)this.framewidth / this.frameheight) > 1.77f)
                                {
                                    this.b旧企画クリップである = false;
                                    this.tx描画用.t2D描画(CDTXMania.app.Device, this.position, 0);
                                    this.tx描画用.t2D描画(CDTXMania.app.Device, 0, this.position);
                                    if (this.tx描画用2 != null)
                                    {
                                        this.tx描画用2.t2D描画(CDTXMania.app.Device, this.position, 0);
                                        this.tx描画用2.t2D描画(CDTXMania.app.Device, 0, this.position);
                                    }

                                }
                                else
                                {
                                    this.b旧企画クリップである = true;
                                    this.tx描画用.vc拡大縮小倍率 = this.vclip;
                                    this.tx描画用.t2D描画(CDTXMania.app.Device, 882, 0);
                                }
                            }
                        }
                    }
                }


            #endregion

                //if (this.b旧企画クリップである == true)
                {
                    CDTXMania.stage演奏ドラム画面.actBGA.t進行描画(980, 0);
                }

                if (CDTXMania.ConfigIni.ボーナス演出を表示する == true)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (this.stフィルイン[i].b使用中)
                        {
                            int numf = this.stフィルイン[i].ct進行.n現在の値;
                            this.stフィルイン[i].ct進行.t進行();
                            if (this.stフィルイン[i].ct進行.b終了値に達した)
                            {
                                this.stフィルイン[i].ct進行.t停止();
                                this.stフィルイン[i].b使用中 = false;
                            }
                            if (this.txフィルインエフェクト != null)
                            {
                                this.txフィルインエフェクト.vc拡大縮小倍率.X = 2.0f;
                                this.txフィルインエフェクト.vc拡大縮小倍率.Y = 2.0f;
                                this.txフィルインエフェクト.b加算合成 = true;
                                this.txフィルインエフェクト.t2D描画(CDTXMania.app.Device, 0, -2, new Rectangle(0, 0 + (360 * numf), 640, 360));
                            }
                        }
                    }
                }

                if (CDTXMania.ConfigIni.bDrums有効 == true)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        int index = this.n描画順[i];
                        int y2 = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 482 : 482)) + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[index].nY座標オフセットdot;
                        int yh = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 495 : 495)) + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[index].nY座標オフセットdot;
                        int yb = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 490 : 490)) - CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[index].nY座標オフセットdot;
                        int yl = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 486 : 486)) + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[index].nY座標オフセットdot;
                        int yf = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 470 : 470)) + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[index].nY座標オフセットdot;
                        if (CDTXMania.ConfigIni.bドラムセットを動かす == false)
                        {
                            y2 = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 482 : 482));
                            yh = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 495 : 495));
                            yb = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 520 : 520));
                            yl = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 486 : 486));
                            yf = (this.st基本位置[index].y + (CDTXMania.ConfigIni.bReverse.Drums ? 470 : 470));
                        }

                        #region[動くドラムセット]

                        if (index == 0)
                        {
                            this.ct左シンバル.t進行();
                            this.tx左シンバル.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0 + (380 * LCym), 0, 380, 720));
                        }
                        if (index == 2)
                        {
                            this.txスネア.t2D描画(CDTXMania.app.Device, 0, y2);
                        }
                        if (index == 4)
                        {
                            this.txハイタム.t2D描画(CDTXMania.app.Device, 106, yh);
                        }
                        if (index == 3)
                        {
                            this.txバスドラ.t2D描画(CDTXMania.app.Device, 310, yb);
                        }
                        if (index == 5)
                        {
                            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                            {
                                this.txロータム.t2D描画(CDTXMania.app.Device, 817, yl);
                            }
                            else
                            {
                                this.txロータム.t2D描画(CDTXMania.app.Device, 870, yl);
                            }
                        }
                        if (index == 6)
                        {
                            if (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.D)
                            {
                                this.txフロアタム.t2D描画(CDTXMania.app.Device, 996, yf);
                            }
                            else
                            {
                                this.txフロアタム.t2D描画(CDTXMania.app.Device, 1049, yf);
                            }
                        }

                        if ((index == 7))
                        {
                            this.ct右シンバル.t進行();
                            this.tx右シンバル.t2D描画(CDTXMania.app.Device, 900, 0, new Rectangle(0 + (380 * RCym), 0, 380, 720));
                        }

                        #endregion


                    }
                    if (CDTXMania.ConfigIni.nMovieAlpha <= 5 || CDTXMania.ConfigIni.nMovieAlpha == 11)
                    {
                        this.txドラム.t2D描画(CDTXMania.app.Device, 0, 0);
                    }
                    int n振動x座標 = 0;
                    if (CDTXMania.stage演奏ドラム画面.bボーナス == true)
                    { 
                        for (int n振動回数 = 0; n振動回数 < 20; n振動回数++)
                        {
                            switch (n振動回数)
                            {
                                case 0:
                                    n振動x座標 = 20;
                                    break;
                                case 2:
                                    n振動x座標 = -20;
                                    break;
                                case 4:
                                    n振動x座標 = 16;
                                    break;
                                case 6:
                                    n振動x座標 = -16;
                                    break;
                                case 8:
                                    n振動x座標 = 10;
                                    break;
                                case 10:
                                    n振動x座標 = -10;
                                    break;
                                case 12:
                                    n振動x座標 = 8;
                                    break;
                                case 14:
                                    n振動x座標 = -8;
                                    break;
                                case 16:
                                    n振動x座標 = 2;
                                    break;
                                case 18:
                                    n振動x座標 = -2;
                                    break;
                                case 20:
                                    n振動x座標 = 0;
                                    break;
                            }
                        }
                        CDTXMania.stage演奏ドラム画面.bボーナス = false;
                    }
                        this.txバートップ.t2D描画(CDTXMania.app.Device, n振動x座標, 0);


                    if ((CDTXMania.ConfigIni.eNamePlate.Drums != Eタイプ.D) && (this.txBPMバー左 != null && this.txBPMバー右 != null))
                    {
                        if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.A)
                        {

                            if (CDTXMania.stage演奏ドラム画面.bサビ区間 == true)
                            {
                                this.txBPMバー左.t2D描画(CDTXMania.app.Device, 232, 54, new Rectangle(0, 0 + (600 * num1), 19, 600));
                                if (this.txBPMバーフラッシュ右 != null && this.txBPMバーフラッシュ左 != null)
                                {
                                    {
                                        this.txBPMバーフラッシュ左.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                        this.txBPMバーフラッシュ左.t2D描画(CDTXMania.app.Device, 209 + (1 * num1), 54);
                                    }
                                }
                            }
                            else
                            {
                                this.txBPMバー左.t2D描画(CDTXMania.app.Device, 232, 54, new Rectangle(0, 0 + (600 * num1), 19, 600));
                            }

                            if (CDTXMania.stage演奏ドラム画面.bサビ区間 == true)
                            {
                                this.txBPMバー右.t2D描画(CDTXMania.app.Device, 896, 54, new Rectangle(0, 0 + (600 * num1), 19, 600));
                                if (this.txBPMバーフラッシュ右 != null && this.txBPMバーフラッシュ左 != null)
                                {
                                    this.txBPMバーフラッシュ右.n透明度 = 255 - (int)(num1 * 18.214285714285714285714285714286);
                                    this.txBPMバーフラッシュ右.t2D描画(CDTXMania.app.Device, 908 - (1 * num1), 54);
                                }
                            }
                            else
                            {
                                this.txBPMバー右.t2D描画(CDTXMania.app.Device, 896, 54, new Rectangle(0, 0 + (600 * num1), 19, 600));
                            }
                        }
                        else if (CDTXMania.ConfigIni.eBPMbar == Eタイプ.B)
                        {
                            this.txBPMバー左.t2D描画(CDTXMania.app.Device, 232, 54, new Rectangle(0, 0 + (600 * num1), 19, 600));
                        }
                    }
                    if (CDTXMania.ConfigIni.bDrums有効 == true)
                    {
                        if (CDTXMania.ConfigIni.eNamePlate.Drums != Eタイプ.D && CDTXMania.ConfigIni.bLivePoint)
                        {
                            #region[LivePoint]
                            if (this.txLivePoint != null)
                                this.txLivePoint.t2D描画(CDTXMania.app.Device, 886, 52, new Rectangle(0, 0, 71, 668));
                            string str = CDTXMania.stage演奏ドラム画面.actAVI.LivePoint.ToString("000");
                            for (int i = 0; i < 3; i++)
                            {
                                Rectangle rectangle;
                                char ch = str[i];
                                if (ch.Equals(' '))
                                {
                                    rectangle = new Rectangle(72, 0, 17, 25);
                                }
                                else
                                {
                                    int num3 = int.Parse(str.Substring(i, 1));
                                    if (num3 < 5)
                                    {
                                        rectangle = new Rectangle(72, (num3 * 25), 17, 25);
                                    }
                                    else
                                    {
                                        rectangle = new Rectangle(72, (num3 * 25), 17, 25);
                                    }
                                }
                                if (this.txScore != null)
                                {
                                    this.txLivePoint.t2D描画(CDTXMania.app.Device, 895 + (i * 18), 685, rectangle);
                                }
                                #region[箱]
                                //まず箱を再現するためにはLPが一定以上になったら表示させるような仕掛けが必要。
                                if (this.LivePoint >= 0)
                                {
                                    if (this.LivePoint >= 20)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 626, new Rectangle(71, 536, 46, 44));
                                    }
                                    if (this.LivePoint >= 40)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 585, new Rectangle(71, 536, 46, 44));
                                    }
                                    if (this.LivePoint >= 60)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 544, new Rectangle(71, 536, 46, 44));
                                    }
                                    if (this.LivePoint >= 80)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 503, new Rectangle(71, 536, 46, 44));
                                    }
                                    if (this.LivePoint >= 100)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 462, new Rectangle(71, 536, 46, 44));
                                    }
                                    if (this.LivePoint >= 120)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 420, new Rectangle(71, 580, 46, 44));
                                    }
                                    if (this.LivePoint >= 140)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 379, new Rectangle(71, 580, 46, 44));
                                    }
                                    if (this.LivePoint >= 160)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 338, new Rectangle(71, 580, 46, 44));
                                    }
                                    if (this.LivePoint >= 180)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 297, new Rectangle(71, 580, 46, 44));
                                    }
                                    if (this.LivePoint >= 200)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 256, new Rectangle(71, 580, 46, 44));
                                    }
                                    if (this.LivePoint >= 220)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 214, new Rectangle(71, 624, 46, 44));
                                    }
                                    if (this.LivePoint >= 240)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 173, new Rectangle(71, 624, 46, 44));
                                    }
                                    if (this.LivePoint >= 260)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 132, new Rectangle(71, 624, 46, 44));
                                    }
                                    if (this.LivePoint >= 280)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 91, new Rectangle(71, 624, 46, 44));
                                    }
                                    if (this.LivePoint >= 300)
                                    {
                                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 887, 50, new Rectangle(71, 624, 46, 44));
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                }

                //コンボ、ネームプレート
                if (CDTXMania.ConfigIni.bGraph.Drums == false && (CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.A || CDTXMania.ConfigIni.eNamePlate.Drums == Eタイプ.C))
                {
                    CDTXMania.stage演奏ドラム画面.actStatusPanels.On進行描画();
                    if(CDTXMania.ConfigIni.ドラムコンボ文字の表示位置 != Eドラムコンボ文字の表示位置.CENTER)
                    CDTXMania.stage演奏ドラム画面.actCombo.On進行描画();
                }

                if ((((this.n移動開始時刻ms != -1L) && (this.rAVI != null)) && (this.rAVI.avi != null)) && (this.bWindowMode) && (CDTXMania.ConfigIni.bAVI有効))
                {
                    this.vector = this.tx描画用.vc拡大縮小倍率;
                    this.tx描画用.vc拡大縮小倍率 = this.smallvc;
                    this.tx描画用.n透明度 = 0xff;
                    if (this.b旧企画クリップである == false)//ワイドクリップ
                    {
                        if (CDTXMania.ConfigIni.bGraph.Drums == true)
                        {
                            if (tx描画用.szテクスチャサイズ.Width >= 1280)
                                this.smallvc = new Vector3(0.2f, 0.2f, 1f);
                            else if(tx描画用.szテクスチャサイズ.Width <= 640)
                                this.smallvc = new Vector3(0.4f, 0.4f, 1f);
                            if (this.txクリップパネル != null)
                                this.txクリップパネル.t2D描画(CDTXMania.app.Device, 4, 401);
                            this.tx描画用.vc拡大縮小倍率 = this.smallvc;
                            this.tx描画用.t2D描画(CDTXMania.app.Device, 13, 484);
                            CDTXMania.stage演奏ドラム画面.actBGA.t進行描画(13, 484);
                        }
                        else
                        {
                            if (this.txクリップパネル != null)
                                this.txクリップパネル.t2D描画(CDTXMania.app.Device, 856, 112);
                            this.tx描画用.t2D描画(CDTXMania.app.Device, 860, 140);
                            CDTXMania.stage演奏ドラム画面.actBGA.t進行描画(860, 140);
                        }
                    }
                    else if (this.b旧企画クリップである == true && CDTXMania.ConfigIni.bGraph.Drums)//旧規格
                    {
                        if (CDTXMania.ConfigIni.bGraph.Drums == true)
                        {
                            this.smallvc = new Vector3(0.76f, 0.76f, 1f);
                            if (this.txクリップパネル != null)
                            {
                                this.txクリップパネル.t2D描画(CDTXMania.app.Device, 4, 401);
                            }
                            this.tx描画用.vc拡大縮小倍率 = this.smallvc;
                            this.tx描画用.t2D描画(CDTXMania.app.Device, 35, 422);
                        }
                        else
                        {
                        }
                    }
                    this.tx描画用.vc拡大縮小倍率 = this.vector;

                }
            }
        Label_0A06:
            if (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true)
                this.txlanes.t2D描画(CDTXMania.app.Device, 0, 0);

            return 0;
        }
        public void Start(bool bフィルイン)
        {
            for (int j = 0; j < 2; j++)
            {
                if (this.stフィルイン[j].b使用中)
                {
                    this.stフィルイン[j].ct進行.t停止();
                    this.stフィルイン[j].b使用中 = false;
                }
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (!this.stフィルイン[j].b使用中)
                    {
                        this.stフィルイン[j].b使用中 = true;
                        this.stフィルイン[j].ct進行 = new CCounter(0, 31, 30, CDTXMania.Timer);
                        break;
                    }
                }
            }
        }
        public override int On進行描画()
        {
            throw new InvalidOperationException("t進行描画(int,int)のほうを使用してください。");
        }


        // その他

        #region [ private ]
        //-----------------
        private bool bFullScreen;
        private Bitmap blanes;
        public bool bWindowMode;
        private bool bフレームを作成した;
        public bool b旧企画クリップである = true;
        private uint frameheight;
        private uint framewidth;
        private int i1;
        private int i2;
        public CCounter ct右シンバル;
        public CCounter ct左シンバル;
        public double LivePoint;
        private Image ilanes;
        private int nAlpha;
        private int nCurrentMovieMode;
        private long n移動開始時刻ms;
        private int n画像側開始位置X;
        private int n画像側開始位置Y;
        private int n画像側終了位置X;
        private int n画像側終了位置Y;
        private int n開始サイズH;
        private int n開始サイズW;
        private int n終了サイズH;
        private int n終了サイズW;
        private int n前回表示したフレーム番号;
        private int n総移動時間ms;
        private int n表示側開始位置X;
        private int n表示側開始位置Y;
        private int n表示側終了位置X;
        private int n表示側終了位置Y;
        private IntPtr pBmp;
        private int position;
        private CDTX.CAVI rAVI;

        private CTexture txBPMバー左;
        private CTexture txBPMバー右;
        private CTexture txBPMバーフラッシュ左;
        private CTexture txBPMバーフラッシュ右;
        private CTexture txlanes;
        private CTexture txLivePoint;
        private CTexture txScore;
        private CTexture txクリップパネル;
        private CTexture txスネア;
        private CTexture txドラム;
        private CTexture txハイタム;
        private CTexture txバートップ;
        private CTexture txバスドラ;
        private CTexture tx左シンバル;
        private CTexture txフロアタム;
        private CTexture txフィルインエフェクト;
        private CTexture tx右シンバル;
        private CTexture txロータム;
        private CTexture tx描画用;
        private CTexture tx描画用2;
        private CCounter[] ct箱 = new CCounter[15];

        private float ratio1;
        private Rectangle rec;
        private Rectangle rec2;
        private Rectangle rec3;
        public Vector3 smallvc;
        private Vector3 vclip;
        public Vector3 vector;

        [StructLayout(LayoutKind.Sequential)]
        private struct STパッド状態
        {
            public int nY座標オフセットdot;
            public int nY座標加速度dot;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct ST基本位置
        {
            public int x;
            public int y;
            public Rectangle rc;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct STフィルイン
        {
            public bool b使用中;
            public CCounter ct進行;
        }
        private STパッド状態[] stパッド状態 = new STパッド状態[19];
        private readonly int[] n描画順 = new int[] { 9, 2, 4, 6, 5, 3, 1, 8, 7, 0 };
        private readonly ST基本位置[] st基本位置;
        public STフィルイン[] stフィルイン = new STフィルイン[2];
        //-----------------
        #endregion
    }
}
