﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using SlimDX.Direct3D9;
using DirectShowLib;
using FDK;

namespace DTXMania
{
    internal class CAct演奏AVI : CActivity
    {
        // コンストラクタ

        public CAct演奏AVI()
        {
            base.b活性化してない = true;
        }


        // メソッド

        public void Start(int nチャンネル番号, CDTX.CAVI rAVI, CDTX.CDirectShow dsBGV, int n開始サイズW, int n開始サイズH, int n終了サイズW, int n終了サイズH, int n画像側開始位置X, int n画像側開始位置Y, int n画像側終了位置X, int n画像側終了位置Y, int n表示側開始位置X, int n表示側開始位置Y, int n表示側終了位置X, int n表示側終了位置Y, int n総移動時間ms, int n移動開始時刻ms)
        {
            if( nチャンネル番号 == 0x54 && CDTXMania.ConfigIni.bDirectShowMode == false && CDTXMania.ConfigIni.bAVI有効 )
            {
                this.rAVI = rAVI;
                //this.dsBGV.dshow.Dispose();
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
                this.n移動開始時刻ms = (n移動開始時刻ms != -1) ? n移動開始時刻ms : CSound管理.rc演奏用タイマ.n現在時刻;
                this.n前回表示したフレーム番号 = -1;
                if ((this.rAVI != null) && (this.rAVI.avi != null))
                {
                    float f拡大率x;
                    float f拡大率y;
                    this.framewidth = this.rAVI.avi.nフレーム幅;
                    this.frameheight = this.rAVI.avi.nフレーム高さ;
                    this.fAVIアスペクト比 = ((float)this.framewidth) / ((float)this.frameheight);
                    if (this.tx描画用 == null)
                    {
                        this.tx描画用 = new CTexture(CDTXMania.app.Device, (int)this.framewidth, (int)this.frameheight, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                    }
                    if ( fAVIアスペクト比 < 1.77f )
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
                        f拡大率x = 420f / ((float)this.framewidth);
                    }
                    else
                    {
                        f拡大率x = 1f;
                    }
                    if (this.frameheight > 580)
                    {
                        f拡大率y = 580f / ((float)this.frameheight);
                    }
                    else
                    {
                        f拡大率y = 1f;
                    }
                    if (f拡大率x > f拡大率y)
                    {
                        f拡大率x = f拡大率y;
                    }
                    else
                    {
                        f拡大率y= f拡大率x;
                    }

                    this.smallvc = new Vector3(f拡大率x, f拡大率y, 1f);
                    this.vclip = new Vector3(1.42f, 1.42f, 1f);
                }
            }
            else if ( nチャンネル番号 == 0x54 && CDTXMania.ConfigIni.bDirectShowMode && CDTXMania.ConfigIni.bAVI有効 )
            {
                this.rAVI = rAVI;
                this.dsBGV = dsBGV;
                if (this.dsBGV != null && this.dsBGV.dshow != null)
                {
                    this.framewidth = (uint)this.dsBGV.dshow.n幅px;
                    this.frameheight = (uint)this.dsBGV.dshow.n高さpx;
                    float f拡大率x;
                    float f拡大率y;
                    this.fAVIアスペクト比 = ((float)this.framewidth) / ((float)this.frameheight);

                    if ( fAVIアスペクト比 < 1.77f )
                    {
                        #region[ 旧規格クリップ時の処理。結果的には面倒な処理なんだよな・・・・ ]
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
                        this.n移動開始時刻ms = (n移動開始時刻ms != -1) ? n移動開始時刻ms : CSound管理.rc演奏用タイマ.n現在時刻;
                        this.n前回表示したフレーム番号 = -1;

                        this.vclip = new Vector3(1.42f, 1.42f, 1f);
                        this.dsBGV = null;
                        #endregion
                    }
                    if (this.tx描画用 == null)
                    {
                        this.tx描画用 = new CTexture(CDTXMania.app.Device, (int)this.framewidth, (int)this.frameheight, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                    }

                    #region[ リサイズ処理 ]
                    if (fAVIアスペクト比 < 1.77f)
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
                        f拡大率x = 420f / ((float)this.framewidth);
                    }
                    else
                    {
                        f拡大率x = 1f;
                    }
                    if (this.frameheight > 580)
                    {
                        f拡大率y = 580f / ((float)this.frameheight);
                    }
                    else
                    {
                        f拡大率y = 1f;
                    }
                    if (f拡大率x > f拡大率y)
                    {
                        f拡大率x = f拡大率y;
                    }
                    else
                    {
                        f拡大率y = f拡大率x;
                    }

                    this.smallvc = new Vector3(f拡大率x, f拡大率y, 1f);
                    #endregion
                }

                if ( fAVIアスペクト比 > 1.77f && this.dsBGV != null && this.dsBGV.dshow != null )
                {
                    this.dsBGV.dshow.t再生開始();
                    this.bDShowクリップを再生している = true;
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
                                this.Start(chip.nチャンネル番号, chip.rAVI, chip.rDShow, 1280, 720, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chip.n発声時刻ms);
                            }
                            continue;
                        }
                    case EAVI種別.AVIPAN:
                        {
                            if (chip.rAVIPan != null)
                            {
                                this.Start(chip.nチャンネル番号, chip.rAVI, chip.rDShow, chip.rAVIPan.sz開始サイズ.Width, chip.rAVIPan.sz開始サイズ.Height, chip.rAVIPan.sz終了サイズ.Width, chip.rAVIPan.sz終了サイズ.Height, chip.rAVIPan.pt動画側開始位置.X, chip.rAVIPan.pt動画側開始位置.Y, chip.rAVIPan.pt動画側終了位置.X, chip.rAVIPan.pt動画側終了位置.Y, chip.rAVIPan.pt表示側開始位置.X, chip.rAVIPan.pt表示側開始位置.Y, chip.rAVIPan.pt表示側終了位置.X, chip.rAVIPan.pt表示側終了位置.Y, chip.n総移動時間, chip.n発声時刻ms);
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
                if (this.dsBGV != null && CDTXMania.ConfigIni.bDirectShowMode == true)
                {
                    this.dsBGV.dshow.MediaCtrl.Stop();
                    this.bDShowクリップを再生している = false;
                }
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
            this.dsBGV = null;
            this.n移動開始時刻ms = -1;
            this.n前回表示したフレーム番号 = -1;
            this.bフレームを作成した = false;
            this.b再生トグル = false;
            this.bDShowクリップを再生している = false;
            this.pBmp = IntPtr.Zero;
            this.MovieMode();
            this.nAlpha = 255 - ((int)(((float)(CDTXMania.ConfigIni.nMovieAlpha * 255)) / 10f));
            if (File.Exists(CSkin.Path(@"Graphics\7_Movie.mp4")))
            {
                this.ds汎用 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(CSkin.Path(@"Graphics\7_Movie.mp4"), CDTXMania.app.WindowHandle, true);
            }
            base.On活性化();
        }
        public override void On非活性化()
        {
            if(this.dsBGV != null)
                this.dsBGV.Dispose();
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txドラム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Drums.png"));
                this.tx黒幕 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\7_Drums_black.png") );
                if (CDTXMania.ConfigIni.bGraph.Drums == true)
                {
                    this.txクリップパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_ClipPanelB.png"));
                }
                else
                {
                    this.txクリップパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_ClipPanel.png"));
                }
                this.txDShow汎用 = new CTexture(CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
                this.txScore = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_score numbers.png"));

                for (int i = 0; i < 1; i++)
                {
                    this.stフィルイン[i] = new STフィルイン();
                    this.stフィルイン[i].ct進行 = new CCounter(0, 31, 30, CDTXMania.Timer);
                    this.stフィルイン[i].b使用中 = false;
                }
                //this.txフィルインエフェクト = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Fillin Effect.png"));
                
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
                    this.tx描画用 = null;
                }
                if (this.tx描画用2 != null)
                {
                    this.tx描画用2.Dispose();
                    this.tx描画用2 = null;
                }
                if (this.txDShow汎用 != null)
                {
                    this.txDShow汎用.Dispose();
                    this.txDShow汎用 = null;
                }
                if (this.txlanes != null)
                {
                    this.txlanes.Dispose();
                    this.txlanes = null;
                }
                CDTXMania.t安全にDisposeする(ref this.ds汎用);
                //テクスチャ 17枚
                CDTXMania.tテクスチャの解放(ref this.txScore);
                CDTXMania.tテクスチャの解放(ref this.txドラム);
                CDTXMania.tテクスチャの解放( ref this.txクリップパネル );
                //CDTXMania.tテクスチャの解放( ref this.txフィルインエフェクト );
                CDTXMania.tテクスチャの解放( ref this.tx黒幕 );
                base.OnManagedリソースの解放();
            }
        }
        public unsafe int t進行描画(int x, int y)
        {
            if (this.txDShow汎用 != null && CDTXMania.stage演奏ドラム画面.ct登場用.b終了値に達した)
            {
                #region[ 汎用動画 ]
                if (this.ds汎用 != null)
                {
                    if (this.txDShow汎用 != null)
                    {
                        this.txDShow汎用.vc拡大縮小倍率 = new Vector3(
                            ((float)1280 / (float)this.ds汎用.n幅px),
                            ((float)720 / (float)this.ds汎用.n高さpx),
                            1.0f);
                    }
                    this.ds汎用.bループ再生 = true;
                    this.ds汎用.t再生開始();
                    this.ds汎用.t現時点における最新のスナップイメージをTextureに転写する(this.txDShow汎用);
                    if ( this.ds汎用.b上下反転 )
                        this.txDShow汎用.t2D上下反転描画( CDTXMania.app.Device, 0, 0 );
                    else
                        this.txDShow汎用.t2D描画( CDTXMania.app.Device, 0, 0 );

                    long l汎用位置;
                    long l汎用終了位置;

                    this.ds汎用.MediaSeeking.GetPositions(out l汎用位置, out l汎用終了位置);
                    if (l汎用位置 == l汎用終了位置)
                    {
                        this.ds汎用.MediaSeeking.SetPositions(
                        DsLong.FromInt64((long)(0)),
                        AMSeekingSeekingFlags.AbsolutePositioning,
                        0,
                        AMSeekingSeekingFlags.NoPositioning);
                    }
                }
                #endregion
            }
            #region[ムービーのフレーム作成処理]
            if ((!base.b活性化してない))
            {
                if (((this.bFullScreen || this.bWindowMode) && (this.tx描画用 != null)) && (this.dsBGV != null || this.rAVI != null)) //クリップ無し曲での進入防止。
                {
                    Rectangle rectangle;
                    Rectangle rectangle2;

                    int time = (int)((CSound管理.rc演奏用タイマ.n現在時刻 - this.n移動開始時刻ms) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                    int frameNoFromTime = 0;

                    #region[ frameNoFromTime ]
                    if ( CDTXMania.ConfigIni.bDirectShowMode == true && (this.dsBGV != null) )
                    {
                        if ( this.fAVIアスペクト比 > 1.77f )
                        {
                            this.dsBGV.dshow.MediaSeeking.GetPositions(out this.lDshowPosition, out this.lStopPosition);
                            frameNoFromTime = (int)lDshowPosition;
                        }
                        else
                        {
                            frameNoFromTime = this.rAVI.avi.GetFrameNoFromTime(time);
                        }
                    }
                    else if ( this.rAVI != null )
                    {
                        frameNoFromTime = this.rAVI.avi.GetFrameNoFromTime(time);
                    }
                    #endregion

                    if ((this.n総移動時間ms != 0) && (this.n総移動時間ms < time))
                    {
                        this.n総移動時間ms = 0;
                        this.n移動開始時刻ms = -1L;
                    }
                    if( ( this.n総移動時間ms == 0 ) && this.rAVI.avi != null ? ( frameNoFromTime >= this.rAVI.avi.GetMaxFrameCount() ) : false )
                    {
                        this.n移動開始時刻ms = -1L;
                    }
                    if ((((this.n前回表示したフレーム番号 != frameNoFromTime) || !this.bフレームを作成した)) && (fAVIアスペクト比 < 1.77f || CDTXMania.ConfigIni.bDirectShowMode == false))
                    {
                        this.pBmp = this.rAVI.avi.GetFramePtr(frameNoFromTime);
                        this.n前回表示したフレーム番号 = frameNoFromTime;
                        this.bフレームを作成した = true;
                    }
                    
                    //ループ防止
                    if ( this.lDshowPosition >= this.lStopPosition && CDTXMania.ConfigIni.bDirectShowMode == true && this.dsBGV != null )
                    {
                        this.dsBGV.dshow.MediaSeeking.SetPositions(
                        DsLong.FromInt64((long)(0)),
                        AMSeekingSeekingFlags.AbsolutePositioning,
                        null,
                        AMSeekingSeekingFlags.NoPositioning);
                        this.dsBGV.dshow.MediaCtrl.Stop();
                        this.bDShowクリップを再生している = false;
                    }

                    Size size = new Size((int)this.framewidth, (int)this.frameheight);
                    Size sz720pサイズ = new Size(1280, 720);
                    Size sz開始サイズ = new Size(this.n開始サイズW, this.n開始サイズH);
                    Size sz終了サイズ = new Size(this.n終了サイズW, this.n終了サイズH);
                    Point location = new Point(this.n画像側開始位置X, this.n画像側終了位置Y);
                    Point point2 = new Point(this.n画像側終了位置X, this.n画像側終了位置Y);
                    Point point3 = new Point(this.n表示側開始位置X, this.n表示側開始位置Y);
                    Point point4 = new Point(this.n表示側終了位置X, this.n表示側終了位置Y);
                    if (CSound管理.rc演奏用タイマ.n現在時刻 < this.n移動開始時刻ms)
                    {
                        this.n移動開始時刻ms = CSound管理.rc演奏用タイマ.n現在時刻;
                    }
                    time = (int)((CSound管理.rc演奏用タイマ.n現在時刻 - this.n移動開始時刻ms) * (((double)CDTXMania.ConfigIni.n演奏速度) / 20.0));
                    if (this.n総移動時間ms == 0)
                    {
                        rectangle = new Rectangle(location, sz開始サイズ);
                        rectangle2 = new Rectangle(point3, sz開始サイズ);
                    }
                    else
                    {
                        double num5 = ((double)time) / ((double)this.n総移動時間ms);
                        Size size5 = new Size(sz開始サイズ.Width + ((int)((sz終了サイズ.Width - sz開始サイズ.Width) * num5)), sz開始サイズ.Height + ((int)((sz終了サイズ.Height - sz開始サイズ.Height) * num5)));
                        rectangle = new Rectangle((int)((point2.X - location.X) * num5), (int)((point2.Y - location.Y) * num5), ((int)((point2.X - location.X) * num5)) + size5.Width, ((int)((point2.Y - location.Y) * num5)) + size5.Height);
                        rectangle2 = new Rectangle((int)((point4.X - point3.X) * num5), (int)((point4.Y - point3.Y) * num5), ((int)((point4.X - point3.X) * num5)) + size5.Width, ((int)((point4.Y - point3.Y) * num5)) + size5.Height);
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
                        if (rectangle2.Right > sz720pサイズ.Width)
                        {
                            int num12 = rectangle2.Right - sz720pサイズ.Width;
                            rectangle.Width -= num12;
                            rectangle2.Width -= num12;
                        }
                        if (rectangle2.Bottom > sz720pサイズ.Height)
                        {
                            int num13 = rectangle2.Bottom - sz720pサイズ.Height;
                            rectangle.Height -= num13;
                            rectangle2.Height -= num13;
                        }
                    }
                    if ((this.tx描画用 != null) && (this.n総移動時間ms != -1) && CDTXMania.ConfigIni.bDirectShowMode == false)
                    {
                        #region[ 通常の動画生成&再生処理 ]
                        if (this.bフレームを作成した && (this.pBmp != IntPtr.Zero))
                        {
                            #region[ フレームの生成 ]
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
                            #endregion
                        }
                        if (this.bFullScreen)
                        {
                            #region[ 動画の描画 ]
                            if (fAVIアスペクト比 > 1.77f)
                            {
                                this.tx描画用.t2D描画(CDTXMania.app.Device, this.position, 0);
                                this.tx描画用.t2D描画(CDTXMania.app.Device, 0, this.position);
                            }
                            else
                            {
                                this.tx描画用.vc拡大縮小倍率 = this.vclip;
                                this.tx描画用.t2D描画(CDTXMania.app.Device, 882, 0);
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else if ((this.tx描画用 != null) && CDTXMania.ConfigIni.bDirectShowMode == true)
                    {
                        #region[ DirectShowMode ]
                        if ( fAVIアスペクト比 > 1.77f && ( this.bDShowクリップを再生している == true ) && this.dsBGV.dshow != null )
                        {
                            #region[ ワイドクリップ ]
                            //this.rAVI.avi.Dispose(); //2013.06.26.kairera0467 DShow時にAVIをメモリから外すことで軽くしたいが、エラーを吐くため保留。
                            this.dsBGV.dshow.t現時点における最新のスナップイメージをTextureに転写する( this.tx描画用 );

                            if( this.bFullScreen )
                            {
                                if( this.dsBGV.dshow.b上下反転 )
                                    this.tx描画用.t2D上下反転描画( CDTXMania.app.Device, this.position, y );
                                else
                                    this.tx描画用.t2D描画( CDTXMania.app.Device, x, y );
                            }
                            #endregion
                        }
                        else if ( fAVIアスペクト比 < 1.77f )
                        {
                            #region[ 旧規格クリップ(DShow未対応) ]
                            if ( this.bフレームを作成した && ( this.pBmp != IntPtr.Zero ) )
                            {
                                DataRectangle rectangle3 = this.tx描画用.texture.LockRectangle( 0, LockFlags.None );
                                DataStream data = rectangle3.Data;
                                int num14 = rectangle3.Pitch / this.tx描画用.szテクスチャサイズ.Width;
                                BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = ( BitmapUtil.BITMAPINFOHEADER* )this.pBmp.ToPointer();
                                if( pBITMAPINFOHEADER->biBitCount == 0x18 )
                                {
                                    switch ( num14 )
                                    {
                                        case 2:
                                            this.rAVI.avi.tBitmap24ToGraphicsStreamR5G6B5( pBITMAPINFOHEADER, data, this.tx描画用.szテクスチャサイズ.Width, this.tx描画用.szテクスチャサイズ.Height );
                                            break;

                                        case 4:
                                            this.rAVI.avi.tBitmap24ToGraphicsStreamX8R8G8B8( pBITMAPINFOHEADER, data, this.tx描画用.szテクスチャサイズ.Width, this.tx描画用.szテクスチャサイズ.Height );
                                            break;
                                    }
                                }
                                this.tx描画用.texture.UnlockRectangle( 0 );
                                this.bフレームを作成した = false;
                                this.tx描画用.vc拡大縮小倍率 = this.vclip;
                            }
                            if( this.bFullScreen )
                                this.tx描画用.t2D描画( CDTXMania.app.Device, 882, 0 );
                            #endregion
                        }
                        #endregion
                    }
                }


            #endregion

                if ( CDTXMania.DTX.listBMP.Count >= 1 && CDTXMania.ConfigIni.bBGA有効 == true)
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
                            if ( this.stフィルイン[i].ct進行.b終了値に達した )
                            {
                                this.stフィルイン[i].ct進行.t停止();
                                this.stフィルイン[i].b使用中 = false;
                            }
                            //if ( this.txフィルインエフェクト != null )
                            CStage演奏ドラム画面 stageDrum = CDTXMania.stage演奏ドラム画面;
                            CStage演奏ギター画面 stageGuitar = CDTXMania.stage演奏ギター画面;

                            if( ( CDTXMania.ConfigIni.bDrums有効 ? stageDrum.txボーナスエフェクト : stageGuitar.txボーナスエフェクト ) != null )
                            {
                                //this.txフィルインエフェクト.vc拡大縮小倍率.X = 2.0f;
                                //this.txフィルインエフェクト.vc拡大縮小倍率.Y = 2.0f;
                                //this.txフィルインエフェクト.b加算合成 = true;
                                //this.txフィルインエフェクト.t2D描画(CDTXMania.app.Device, 0, -2, new Rectangle(0, 0 + (360 * numf), 640, 360));
                                if( CDTXMania.ConfigIni.bDrums有効 )
                                {
                                    stageDrum.txボーナスエフェクト.vc拡大縮小倍率 = new Vector3( 2.0f, 2.0f, 1.0f );
                                    stageDrum.txボーナスエフェクト.b加算合成 = true;
                                    stageDrum.txボーナスエフェクト.t2D描画( CDTXMania.app.Device, 0, -2, new Rectangle(0, 0 + ( 360 * numf ), 640, 360 )) ;
                                }
                            }
                        }
                    }
                }
                if (CDTXMania.ConfigIni.bDrums有効 == true)
                {
                    if ( this.txドラム != null )
                    {
                        this.txドラム.t2D描画(CDTXMania.app.Device, 0, 0);
                    }
                }

                //コンボ、ジャケット画像
                CDTXMania.stage演奏ドラム画面.actStatusPanels.On進行描画();
                CDTXMania.stage演奏ドラム画面.actCombo.On進行描画();


                if ((((this.n総移動時間ms != -1L) && (((this.rAVI != null )) && (this.rAVI.avi != null)) || (this.dsBGV != null && this.dsBGV.dshow != null) )) && ( this.bWindowMode ) && (CDTXMania.ConfigIni.bAVI有効))
                {
                    this.vector = this.tx描画用.vc拡大縮小倍率;
                    this.tx描画用.vc拡大縮小倍率 = this.smallvc;
                    this.tx描画用.n透明度 = 0xff;
                    #region[ スキルメーター有効 ]
                    if ( CDTXMania.ConfigIni.bGraph.Drums == true && CDTXMania.ConfigIni.bDrums有効 )
                    {
                        if ( this.fAVIアスペクト比 > 1.77f )
                        {
                            this.ratio2 = 256f / ((float)this.framewidth);
                            this.position2 = 422 + (int)((266f - (this.frameheight * this.ratio2)) / 2f);
                        }
                        else
                        {
                            this.ratio2 = 266f / ((float)this.frameheight);
                            this.position2 = 13 + (int)((256 - (this.framewidth * this.ratio2)) / 2f);
                        }
                        if (this.txクリップパネル != null)
                            this.txクリップパネル.t2D描画(CDTXMania.app.Device, 4, 401);
                        this.smallvc = new Vector3(this.ratio2, this.ratio2, 1f);

                        if( CDTXMania.ConfigIni.bDirectShowMode == true )
                        {
                            if( this.dsBGV != null && this.bDShowクリップを再生している )
                            {
                                this.dsBGV.dshow.t現時点における最新のスナップイメージをTextureに転写する( this.tx描画用 );
                                if( this.dsBGV.dshow.b上下反転 )
                                {
                                    this.tx描画用.t2D上下反転描画( CDTXMania.app.Device, 13, this.position2 );
                                }
                                else if( this.dsBGV != null && CDTXMania.ConfigIni.bDirectShowMode == true )
                                {
                                    this.tx描画用.t2D描画( CDTXMania.app.Device, 13, this.position2 );
                                }
                            }
                            else if( this.n総移動時間ms != -1 && this.fAVIアスペクト比 < 1.77f )
                            {
                                this.tx描画用.t2D描画( CDTXMania.app.Device, 13, this.position2 );
                            }
                        }
                        else if( CDTXMania.ConfigIni.bDirectShowMode == true && this.n総移動時間ms != -1 )
                        {
                            if ( this.fAVIアスペクト比 < 1.77f )
                            {
                                //this.tx描画用.t2D描画( CDTXMania.app.Device, this.position2, 168 );
                            }
                            else if (this.fAVIアスペクト比 > 1.77f)
                            {
                                //this.tx描画用.t2D描画( CDTXMania.app.Device, 13, this.position2 );
                            }
                        }
                        else if( CDTXMania.ConfigIni.bDirectShowMode == false )
                        {
                            if ( this.fAVIアスペクト比 < 1.77f )
                            {
                                this.tx描画用.t2D描画( CDTXMania.app.Device, this.position2, 168 );
                            }
                            else if (this.fAVIアスペクト比 > 1.77f)
                            {
                                this.tx描画用.t2D描画( CDTXMania.app.Device, 13, this.position2 );
                            }
                        }
                    }
                    #endregion
                    #region[ スキルメーター無効 ]
                    else if( CDTXMania.ConfigIni.bGraph.Drums == false && CDTXMania.ConfigIni.bDrums有効 )
                    {
                        if ( this.fAVIアスペクト比 > 1.77f )
                        {
                            this.ratio2 = 420f / ((float)this.framewidth);
                            this.position2 = 168 + (int)((240f - (this.frameheight * this.ratio2)) / 2f);
                        }
                        else
                        {
                            this.ratio2 = 240f / ((float)this.frameheight);
                            this.position2 = 858 + (int)((420f - (this.framewidth * this.ratio2)) / 2f);
                        }
                        if (this.txクリップパネル != null)
                            this.txクリップパネル.t2D描画(CDTXMania.app.Device, 856, 142); 
                        this.smallvc = new Vector3(this.ratio2, this.ratio2, 1f);
                        this.tx描画用.vc拡大縮小倍率 = this.smallvc;
                        if( CDTXMania.ConfigIni.bDirectShowMode == true  )
                        {
                            if( this.dsBGV != null && this.bDShowクリップを再生している )
                            {
                                this.dsBGV.dshow.t現時点における最新のスナップイメージをTextureに転写する( this.tx描画用 );
                                if( this.dsBGV.dshow.b上下反転 )
                                {
                                    this.tx描画用.t2D上下反転描画( CDTXMania.app.Device, 858, this.position2 );
                                }
                                else if( this.dsBGV != null && CDTXMania.ConfigIni.bDirectShowMode == true )
                                {
                                    this.tx描画用.t2D描画( CDTXMania.app.Device, 858, this.position2 );
                                }
                            }
                            else if( this.n総移動時間ms != -1 && this.fAVIアスペクト比 < 1.77f )
                            {
                                this.tx描画用.t2D描画( CDTXMania.app.Device, this.position2, 168 );
                            }
                        }
                        else if( CDTXMania.ConfigIni.bDirectShowMode == true && this.n総移動時間ms != -1 )
                        {
                            if( this.fAVIアスペクト比 < 1.77f )
                            {
                                //this.tx描画用.t2D描画( CDTXMania.app.Device, this.position2, 168 );
                            }
                            else if( this.fAVIアスペクト比 > 1.77f )
                            {
                                //this.tx描画用.t2D描画( CDTXMania.app.Device, 858, this.position2 );
                            }
                        }
                        else if( CDTXMania.ConfigIni.bDirectShowMode == false )
                        {
                            if( this.fAVIアスペクト比 < 1.77f )
                            {
                                this.tx描画用.t2D描画( CDTXMania.app.Device, this.position2, 168 );
                            }
                            else if(this.fAVIアスペクト比 > 1.77f)
                            {
                                this.tx描画用.t2D描画( CDTXMania.app.Device, 858, this.position2 );
                            }
                        }
                    }
                    #endregion
                    this.tx描画用.vc拡大縮小倍率 = this.vector;
                }
                IInputDevice keyboard = CDTXMania.Input管理.Keyboard;
                if (keyboard.bキーが押された((int)SlimDX.DirectInput.Key.F1))
                {
                    if (this.b再生トグル == false)
                    {
                        if ( this.dsBGV != null )
                        {
                            if( this.dsBGV.dshow != null )
                                this.dsBGV.dshow.MediaCtrl.Pause();
                        }
                        if (this.ds汎用 != null)
                        {
                            this.ds汎用.MediaCtrl.Pause();
                        }
                        this.b再生トグル = true;
                    }
                    else if(this.b再生トグル == true)
                    {
                        if (this.dsBGV != null)
                        {
                            if( this.dsBGV.dshow != null )
                                this.dsBGV.dshow.MediaCtrl.Run();
                        }
                        if (this.ds汎用 != null)
                        {
                            this.ds汎用.MediaCtrl.Run();
                        }
                        this.b再生トグル = false;
                    }
                }
            }

            return 0;
        }
        public void Start(bool bフィルイン)
        {
            for (int j = 0; j < 1; j++)
            {
                if (this.stフィルイン[j].b使用中)
                {
                    this.stフィルイン[j].ct進行.t停止();
                    this.stフィルイン[j].b使用中 = false;
                }
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
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
//      private Bitmap blanes;
        public bool bWindowMode;
        private bool bフレームを作成した;
        private bool b再生トグル;
        private bool bDShowクリップを再生している;
        public float fAVIアスペクト比;
        private uint frameheight;
        private uint framewidth;
        private int i1;
        private int i2;
        public STDGBVALUE<double> LivePoint;
//      private Image ilanes;
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
        private long lDshowPosition;
        private long lStopPosition;
        public IntPtr pBmp;
        private int position;
        private int position2;
        private CDTX.CAVI rAVI;
        private CDirectShow ds汎用;

        public CDTX.CDirectShow dsBGV;

        private CTexture tx黒幕;
        private CTexture txlanes;
        private CTexture txScore;
        private CTexture txクリップパネル;
        private CTexture txドラム;
        //private CTexture txフィルインエフェクト;
        private CTexture tx描画用;
        private CTexture tx描画用2;
        private CTexture txDShow汎用;
        private CCounter[] ct箱 = new CCounter[15];

        private float ratio1;
        private float ratio2;
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
        public struct STフィルイン
        {
            public bool b使用中;
            public CCounter ct進行;
        }
        private STパッド状態[] stパッド状態 = new STパッド状態[19];
        private readonly int[] n描画順 = new int[] { 9, 2, 4, 6, 5, 3, 1, 8, 7, 0 };
        public STフィルイン[] stフィルイン = new STフィルイン[2];
        //-----------------
        #endregion
    }
}
