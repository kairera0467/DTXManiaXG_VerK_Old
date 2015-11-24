using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;
using DirectShowLib;

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

		public void Start( int nチャンネル番号, CDTX.CAVI rAVI, CDTX.CDirectShow dsBGV, int n開始サイズW, int n開始サイズH, int n終了サイズW, int n終了サイズH, int n画像側開始位置X, int n画像側開始位置Y, int n画像側終了位置X, int n画像側終了位置Y, int n表示側開始位置X, int n表示側開始位置Y, int n表示側終了位置X, int n表示側終了位置Y, int n総移動時間ms, int n移動開始時刻ms )
		{
            this.b再生トグル = true;
			if ( ( nチャンネル番号 == 0x54 || nチャンネル番号 == 0x5A ) && !CDTXMania.ConfigIni.bDirectShowMode )
			{
                this.rAVI = rAVI;

                this.n総移動時間ms = n総移動時間ms;
                this.n移動開始時刻ms = (n移動開始時刻ms != -1) ? n移動開始時刻ms : CSound管理.rc演奏用タイマ.n現在時刻;
                this.n前回表示したフレーム番号 = -1;
                if ( ( this.rAVI != null ) && ( this.rAVI.avi != null ) )
                {
                    float f拡大率x;
                    float f拡大率y;
                    this.framewidth = this.rAVI.avi.nフレーム幅;
                    this.frameheight = this.rAVI.avi.nフレーム高さ;
                    this.fAVIアスペクト比 = ( ( float )this.framewidth ) / ( ( float )this.frameheight );
                    if( this.tx描画用 == null )
                    {
                        this.tx描画用 = new CTexture( CDTXMania.app.Device, ( int )this.framewidth, ( int )this.frameheight, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
                    }
                    if( fAVIアスペクト比 < 1.77f )
                    {
                        //旧規格クリップだった場合
                        this.ratio1 = 720f / ( ( float )this.frameheight);
                        this.position = ( int )( ( 1280f - (this.framewidth * this.ratio1 ) ) / 2f );
                        int num = ( int )( this.framewidth * this.ratio1);
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

                    this.vcウィンドウクリップ縮小率 = new Vector3(f拡大率x, f拡大率y, 1f);
                    this.vclip = new Vector3(1.42f, 1.42f, 1f);
                }
			}
            else if ( ( nチャンネル番号 == 0x54 || nチャンネル番号 == 0x5A ) && CDTXMania.ConfigIni.bDirectShowMode && CDTXMania.ConfigIni.bAVI有効 )
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
                    if( fAVIアスペクト比 < 1.77f )
                    {
                        //旧規格クリップだった場合
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

                    this.vcウィンドウクリップ縮小率 = new Vector3(f拡大率x, f拡大率y, 1f);
                    #endregion
                }

                if ( fAVIアスペクト比 > 1.77f && this.dsBGV != null && this.dsBGV.dshow != null )
                {
                    this.dsBGV.dshow.t再生開始();
                    this.bDShowクリップを再生している = true;
                }
            }
		}
		public void SkipStart( int n移動開始時刻ms )
		{
			foreach ( CDTX.CChip chip in CDTXMania.DTX.listChip )
			{
				if ( chip.n発声時刻ms > n移動開始時刻ms )
				{
					break;
				}
				switch ( chip.eAVI種別 )
				{
					case EAVI種別.AVI:
						{
							if ( chip.rAVI != null )
							{
								this.Start( chip.nチャンネル番号, chip.rAVI, chip.rDShow, 1280, 720, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chip.n発声時刻ms );
							}
							continue;
						}
					case EAVI種別.AVIPAN:
						{
							if ( chip.rAVIPan != null )
							{
								this.Start( chip.nチャンネル番号, chip.rAVI, chip.rDShow, chip.rAVIPan.sz開始サイズ.Width, chip.rAVIPan.sz開始サイズ.Height, chip.rAVIPan.sz終了サイズ.Width, chip.rAVIPan.sz終了サイズ.Height, chip.rAVIPan.pt動画側開始位置.X, chip.rAVIPan.pt動画側開始位置.Y, chip.rAVIPan.pt動画側終了位置.X, chip.rAVIPan.pt動画側終了位置.Y, chip.rAVIPan.pt表示側開始位置.X, chip.rAVIPan.pt表示側開始位置.Y, chip.rAVIPan.pt表示側終了位置.X, chip.rAVIPan.pt表示側終了位置.Y, chip.n総移動時間, chip.n発声時刻ms );
							}
							continue;
						}
				}
			}
		}
		public void Stop()
		{
			if ( ( this.rAVI != null ) && ( this.rAVI.avi != null ) )
			{
				this.n移動開始時刻ms = -1;
			}

            if( this.dsBGV != null && CDTXMania.ConfigIni.bDirectShowMode == true )
            {
                this.dsBGV.dshow.MediaCtrl.Stop();
                this.bDShowクリップを再生している = false;
            }
		}
		public void Cont( int n再開時刻ms )
		{
			if ( ( this.rAVI != null ) && ( this.rAVI.avi != null ) )
			{
				this.n移動開始時刻ms = n再開時刻ms;
			}
		}
		public unsafe int t進行描画( int x, int y )
		{
			if ( !base.b活性化してない )
			{
                if( !this.bDShowクリップを再生している || ( this.dsBGV.dshow == null || this.dsBGV == null ) )
                {
				    if( ( ( this.n移動開始時刻ms == -1 ) || ( this.rAVI == null ) ) || ( this.rAVI.avi == null ) )
				    {
    					return 0;
	    			}
                }
				if ( this.tx描画用 == null )
				{
					return 0;
				}
				int time = (int) ( ( CSound管理.rc演奏用タイマ.n現在時刻 - this.n移動開始時刻ms ) * ( ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0 ) );
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

				if ( ( this.n総移動時間ms != 0 ) && ( this.n総移動時間ms < time ) )
				{
					this.n総移動時間ms = 0;
					this.n移動開始時刻ms = -1;
					return 0;
				}

                //2014.11.17 kairera0467 AVIが無い状態でAVIのフレームカウントをさせるとエラーを吐くため、かなり雑ではあるが対策。
                if( ( this.n総移動時間ms == 0 ) && this.rAVI.avi != null ? ( frameNoFromTime >= this.rAVI.avi.GetMaxFrameCount() ) : false )
                {
                    this.n移動開始時刻ms = -1L;
                }
                if((((this.n前回表示したフレーム番号 != frameNoFromTime) || !this.bフレームを作成した)) && (fAVIアスペクト比 < 1.77f || CDTXMania.ConfigIni.bDirectShowMode == false))
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

                #region[ フレーム幅 ]
                //uintじゃなくてint。DTXHDでは無駄に変換してたね。
                int nフレーム幅 = 0;
                int nフレーム高さ = 0;

                if( this.dsBGV != null )
                {
                   nフレーム幅 = this.dsBGV.dshow.n幅px;
                   nフレーム高さ = this.dsBGV.dshow.n高さpx;
                }
                else if( this.rAVI != null || this.rAVI.avi != null )
                {
                    nフレーム幅 = (int)this.rAVI.avi.nフレーム幅;
                    nフレーム高さ = (int)this.rAVI.avi.nフレーム高さ;
                }
                #endregion

                //2014.11.17 kairera0467 フレーム幅をrAVIから参照していたため、先にローカル関数で決めるよう変更。
				Size szフレーム幅 = new Size( nフレーム幅, nフレーム高さ );
				Size sz最大フレーム幅 = new Size( 1280, 720 );
				long num3 = this.n総移動時間ms;
				long num4 = this.n移動開始時刻ms;
				if ( CSound管理.rc演奏用タイマ.n現在時刻 < num4 )
				{
					num4 = CSound管理.rc演奏用タイマ.n現在時刻;
				}
				time = (int) ( ( CSound管理.rc演奏用タイマ.n現在時刻 - num4 ) * ( ( (double) CDTXMania.ConfigIni.n演奏速度 ) / 20.0 ) );
                if( ( this.tx描画用 != null ) && CDTXMania.ConfigIni.bDirectShowMode == true )
                {
                    if ( ( this.bDShowクリップを再生している == true ) && this.dsBGV.dshow != null )
                    {
                        if( CDTXMania.ConfigIni.bDrums有効 == true ) //ウィンドウクリップは外に出す
                            this.dsBGV.dshow.t現時点における最新のスナップイメージをTextureに転写する( CDTXMania.stage演奏ドラム画面.actWinClip.txClip );

                        if( CDTXMania.ConfigIni.eClipDispType == EClipDispType.背景のみ || CDTXMania.ConfigIni.eClipDispType == EClipDispType.両方 )
                        {
                            #region[ ワイドクリップ ]
                            this.dsBGV.dshow.t現時点における最新のスナップイメージをTextureに転写する( this.tx描画用 );

                            if( this.dsBGV.dshow.b上下反転 )
                                this.tx描画用.t2D上下反転描画( CDTXMania.app.Device, x, y );
                            else
                                this.tx描画用.t2D描画( CDTXMania.app.Device, x, y );
                            #endregion
                        }
                    }
                }
				else if ( ( this.tx描画用 != null ) && ( this.n総移動時間ms != -1 ) )
				{
					if ( this.bフレームを作成した && ( this.pBmp != IntPtr.Zero ) )
					{
						DataRectangle rectangle3 = this.tx描画用.texture.LockRectangle( 0, LockFlags.None );
						DataStream data = rectangle3.Data;
						int num14 = rectangle3.Pitch / this.tx描画用.szテクスチャサイズ.Width;
						BitmapUtil.BITMAPINFOHEADER* pBITMAPINFOHEADER = (BitmapUtil.BITMAPINFOHEADER*) this.pBmp.ToPointer();
						if ( pBITMAPINFOHEADER->biBitCount == 0x18 )
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
					}
                    double dbAVI比率 = this.rAVI.avi.nフレーム幅 / this.rAVI.avi.nフレーム高さ;

                    //とりあえず16:9以外は再生しない。
                    if( dbAVI比率 < 1.77 )
					    this.tx描画用.t2D描画( CDTXMania.app.Device, 0, 0 );
                }


                #region[キー入力処理]
			    IInputDevice keyboard = CDTXMania.Input管理.Keyboard;
			    if ( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.F1 ) &&
				    ( keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.RightShift ) || keyboard.bキーが押されている( (int) SlimDX.DirectInput.Key.LeftShift ) ) )
                {
                    if( this.b再生トグル == false )
                    {
                        if( this.dsBGV != null )
                        {
                            if( this.dsBGV.dshow != null )
                                this.dsBGV.dshow.MediaCtrl.Pause();
                        }
                        this.b再生トグル = true;
                    }
                    else if( this.b再生トグル == true )
                    {
                        if(this.dsBGV != null )
                        {
                            if( this.dsBGV.dshow != null )
                                this.dsBGV.dshow.MediaCtrl.Run();
                        }
                        this.b再生トグル = false;
                    }
                }
                #endregion
            }
			return 0;
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.rAVI = null;
			this.n移動開始時刻ms = -1;
			this.n前回表示したフレーム番号 = -1;
			this.bフレームを作成した = false;
			this.pBmp = IntPtr.Zero;
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if ( !base.b活性化してない )
			{
#if TEST_Direct3D9Ex
				this.tx描画用 = new CTexture( CDTXMania.app.Device, 320, 355, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Default, Usage.Dynamic );
#else
				this.tx描画用 = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
#endif
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if ( !base.b活性化してない )
			{
				if ( this.tx描画用 != null )
				{
					this.tx描画用.Dispose();
					this.tx描画用 = null;
				}
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			//throw new InvalidOperationException( "t進行描画(int,int)のほうを使用してください。" );
            return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private bool bフレームを作成した;
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

        public float fAVIアスペクト比;
        private uint frameheight;
        private uint framewidth;
        private int i1;
        private int i2;
        private int position;
        private int position2;
        private float ratio1;
        private float ratio2;
        private Rectangle rec;
        private Rectangle rec2;
        private Rectangle rec3;
        public Vector3 vcウィンドウクリップ縮小率;
        private Vector3 vclip;
        public Vector3 vector;

		private IntPtr pBmp;
		private CDTX.CAVI rAVI;
		public CTexture tx描画用;

        //DirectShow用
        private bool b再生トグル;
        private bool bDShowクリップを再生している;
        private long lDshowPosition;
        private long lStopPosition;

        public CDTX.CDirectShow dsBGV;

		//-----------------
		#endregion
	}
}
