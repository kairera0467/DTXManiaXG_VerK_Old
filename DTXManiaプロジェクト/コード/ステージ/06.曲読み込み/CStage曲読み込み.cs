using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using System.Drawing.Text;
using DirectShowLib;
using FDK;

namespace DTXMania
{
	internal class CStage曲読み込み : CStage
	{
		// コンストラクタ

		public CStage曲読み込み()
		{
			base.eステージID = CStage.Eステージ.曲読み込み;
			base.eフェーズID = CStage.Eフェーズ.共通_通常状態;
			base.b活性化してない = true;
//			base.list子Activities.Add( this.actFI = new CActFIFOBlack() );	// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
			base.list子Activities.Add( this.actFO = new CActFIFOBlackStart() );

            #region[ 難易度数字 ]
            ST文字位置[] st文字位置Array2 = new ST文字位置[11];
            ST文字位置 st文字位置12 = new ST文字位置();
            st文字位置12.ch = '0';
            st文字位置12.pt = new Point(13, 40);
            st文字位置Array2[0] = st文字位置12;
            ST文字位置 st文字位置13 = new ST文字位置();
            st文字位置13.ch = '1';
            st文字位置13.pt = new Point(26, 40);
            st文字位置Array2[1] = st文字位置13;
            ST文字位置 st文字位置14 = new ST文字位置();
            st文字位置14.ch = '2';
            st文字位置14.pt = new Point(39, 40);
            st文字位置Array2[2] = st文字位置14;
            ST文字位置 st文字位置15 = new ST文字位置();
            st文字位置15.ch = '3';
            st文字位置15.pt = new Point(52, 40);
            st文字位置Array2[3] = st文字位置15;
            ST文字位置 st文字位置16 = new ST文字位置();
            st文字位置16.ch = '4';
            st文字位置16.pt = new Point(65, 40);
            st文字位置Array2[4] = st文字位置16;
            ST文字位置 st文字位置17 = new ST文字位置();
            st文字位置17.ch = '5';
            st文字位置17.pt = new Point(78, 40);
            st文字位置Array2[5] = st文字位置17;
            ST文字位置 st文字位置18 = new ST文字位置();
            st文字位置18.ch = '6';
            st文字位置18.pt = new Point(91, 40);
            st文字位置Array2[6] = st文字位置18;
            ST文字位置 st文字位置19 = new ST文字位置();
            st文字位置19.ch = '7';
            st文字位置19.pt = new Point(105, 40);
            st文字位置Array2[7] = st文字位置19;
            ST文字位置 st文字位置20 = new ST文字位置();
            st文字位置20.ch = '8';
            st文字位置20.pt = new Point(118, 40);
            st文字位置Array2[8] = st文字位置20;
            ST文字位置 st文字位置21 = new ST文字位置();
            st文字位置21.ch = '9';
            st文字位置21.pt = new Point(131, 40);
            st文字位置Array2[9] = st文字位置21;
            ST文字位置 st文字位置22 = new ST文字位置();
            st文字位置22.ch = '-';
            st文字位置22.pt = new Point(0,40);
            st文字位置Array2[10] = st文字位置22;
            this.st小文字位置 = st文字位置Array2;

            //大文字
            ST文字位置[] st文字位置Array3 = new ST文字位置[12];
            ST文字位置 st文字位置23 = new ST文字位置();
            st文字位置23.ch = '.';
            st文字位置23.pt = new Point(144, 40);
            st文字位置Array3[0] = st文字位置23;
            ST文字位置 st文字位置24 = new ST文字位置();
            st文字位置24.ch = '1';
            st文字位置24.pt = new Point(22, 0);
            st文字位置Array3[1] = st文字位置24;
            ST文字位置 st文字位置25 = new ST文字位置();
            st文字位置25.ch = '2';
            st文字位置25.pt = new Point(44, 0);
            st文字位置Array3[2] = st文字位置25;
            ST文字位置 st文字位置26 = new ST文字位置();
            st文字位置26.ch = '3';
            st文字位置26.pt = new Point(66, 0);
            st文字位置Array3[3] = st文字位置26;
            ST文字位置 st文字位置27 = new ST文字位置();
            st文字位置27.ch = '4';
            st文字位置27.pt = new Point(88, 0);
            st文字位置Array3[4] = st文字位置27;
            ST文字位置 st文字位置28 = new ST文字位置();
            st文字位置28.ch = '5';
            st文字位置28.pt = new Point(110, 0);
            st文字位置Array3[5] = st文字位置28;
            ST文字位置 st文字位置29 = new ST文字位置();
            st文字位置29.ch = '6';
            st文字位置29.pt = new Point(132, 0);
            st文字位置Array3[6] = st文字位置29;
            ST文字位置 st文字位置30 = new ST文字位置();
            st文字位置30.ch = '7';
            st文字位置30.pt = new Point(153, 0);
            st文字位置Array3[7] = st文字位置30;
            ST文字位置 st文字位置31 = new ST文字位置();
            st文字位置31.ch = '8';
            st文字位置31.pt = new Point(176, 0);
            st文字位置Array3[8] = st文字位置31;
            ST文字位置 st文字位置32 = new ST文字位置();
            st文字位置32.ch = '9';
            st文字位置32.pt = new Point(198, 0);
            st文字位置Array3[9] = st文字位置32;
            ST文字位置 st文字位置33 = new ST文字位置();
            st文字位置33.ch = '0';
            st文字位置33.pt = new Point(220, 0);
            st文字位置Array3[10] = st文字位置33;
            ST文字位置 st文字位置34 = new ST文字位置();
            st文字位置34.ch = '-';
            st文字位置34.pt = new Point(0, 0);
            st文字位置Array3[11] = st文字位置34;
            this.st大文字位置 = st文字位置Array3;
            #endregion

            this.stパネルマップ = null;
            this.stパネルマップ = new STATUSPANEL[12];		// yyagi: 以下、手抜きの初期化でスマン
            string[] labels = new string[12] {
            "DTXMANIA",     //0
            "DEBUT",        //1
            "NOVICE",       //2
            "REGULAR",      //3
            "EXPERT",       //4
            "MASTER",       //5
            "BASIC",        //6
            "ADVANCED",     //7
            "EXTREME",      //8
            "RAW",          //9
            "RWS",          //10
            "REAL"          //11
            };
            int[] status = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            for (int i = 0; i < 12; i++)
            {
                this.stパネルマップ[i] = default(STATUSPANEL);
                this.stパネルマップ[i].status = status[i];
                this.stパネルマップ[i].label = labels[i];
            }
		}

        public void Start()
		{
			if(( this.txBall != null ) )
			{
				for( int i = 0; i < 8; i++ )
				{
					for( int j = 0; j < 8; j++ )
					{
						if( !this.st泡[ j ].b使用中 )
						{
							this.st泡[ j ].b使用中 = true;
							int n回転初期値 = CDTXMania.Random.Next( 360 );
							double num7 = 0.9 + ( ( (double) CDTXMania.Random.Next( 40 ) ) / 100.0 );
							this.st泡[ j ].ct進行 = new CCounter( 0, 200, 3, CDTXMania.Timer );
                            this.st泡[ j ].fX = CDTXMania.Random.Next(1280);
                            this.st泡[ j ].fY = CDTXMania.Random.Next(720);
							//this.st泡[ j ].f半径 = (float) ( 0.5 + ( ( (double) CDTXMania.Random.Next( 30 ) ) / 100.0 ) );
							break;
						}
					}
				}
			}
		}

		// CStage 実装

        public void tラベル名からステータスパネルを決定する(string strラベル名)
        {
            if (string.IsNullOrEmpty(strラベル名))
            {
                this.nIndex = 0;
            }
            else
            {
                STATUSPANEL[] array = this.stパネルマップ;
                for (int i = 0; i < array.Length; i++)
                {
                    STATUSPANEL sTATUSPANEL = array[i];
                    if (strラベル名.Equals(sTATUSPANEL.label, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.nIndex = sTATUSPANEL.status;
                        CDTXMania.nSongDifficulty = sTATUSPANEL.status;
                        return;
                    }
                    this.nIndex++;
                }
            }
        }

		public override void On活性化()
		{
			Trace.TraceInformation( "曲読み込みステージを活性化します。" );
			Trace.Indent();
            FontStyle regular = FontStyle.Regular;
			try
			{
				this.str曲タイトル = "";
                this.strアーティスト名 = "";
				this.strSTAGEFILE = "";
                this.bSTAGEFILEが存在する = false;

                //2013.05.10.kairera0467.曲選択から持ってきた。
                if (CDTXMania.ConfigIni.b選曲リストフォントを斜体にする) regular |= FontStyle.Italic;
                if (CDTXMania.ConfigIni.b選曲リストフォントを太字にする) regular |= FontStyle.Bold;
                this.ftタイトル表示用フォント = new Font( CDTXMania.ConfigIni.str選曲リストフォント, 40f, FontStyle.Bold, GraphicsUnit.Pixel);
                this.ftアーティスト名表示フォント = new Font( CDTXMania.ConfigIni.str選曲リストフォント, 40f, FontStyle.Bold, GraphicsUnit.Pixel);

                if ( File.Exists( CSkin.Path(@"Graphics\6_background.mp4") ) && !CDTXMania.bコンパクトモード )
                {
                    this.ds背景動画 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(CSkin.Path(@"Graphics\6_background.mp4"), CDTXMania.app.WindowHandle, true);
                }

				this.nBGM再生開始時刻 = -1L;
				this.nBGMの総再生時間ms = 0;
				if( this.sd読み込み音 != null )
				{
					CDTXMania.Sound管理.tサウンドを破棄する( this.sd読み込み音 );
					this.sd読み込み音 = null;
				}

				string strDTXファイルパス = ( CDTXMania.bコンパクトモード ) ?
					CDTXMania.strコンパクトモードファイル : CDTXMania.stage選曲.r確定されたスコア.ファイル情報.ファイルの絶対パス;

                CDTX cdtx = new CDTX( strDTXファイルパス, true );

				this.str曲タイトル = ( CDTXMania.bコンパクトモード ) ? cdtx.TITLE : CDTXMania.stage選曲.r確定された曲.strタイトル;
                this.strアーティスト名 = cdtx.ARTIST;
                for (int i = 0; i < 8; i++)
                {
                    this.st泡[i] = new ST泡();
                    this.st泡[i].b使用中 = false;
                    this.st泡[i].ct進行 = new CCounter();
                }
				if( ( ( cdtx.STAGEFILE != null ) && ( cdtx.STAGEFILE.Length > 0 ) ) && ( File.Exists( cdtx.strフォルダ名 + cdtx.STAGEFILE ) && !CDTXMania.ConfigIni.bストイックモード ) )
				{
					this.strSTAGEFILE = cdtx.strフォルダ名 + cdtx.STAGEFILE;
                    this.bSTAGEFILEが存在する = true;
				}
				else
				{
					this.strSTAGEFILE = CSkin.Path( @"Graphics\\6_background.jpg" );
				}
				if( ( ( cdtx.SOUND_NOWLOADING != null ) && ( cdtx.SOUND_NOWLOADING.Length > 0 ) ) && File.Exists( cdtx.strフォルダ名 + cdtx.SOUND_NOWLOADING ) )
				{
					string strNowLoadingサウンドファイルパス = cdtx.strフォルダ名 + cdtx.SOUND_NOWLOADING;
					try
					{
						this.sd読み込み音 = CDTXMania.Sound管理.tサウンドを生成する( strNowLoadingサウンドファイルパス );
					}
					catch
					{
						Trace.TraceError( "#SOUND_NOWLOADING に指定されたサウンドファイルの読み込みに失敗しました。({0})", strNowLoadingサウンドファイルパス );
					}
				}
                int LEVEL = cdtx.LEVEL.Drums;
				cdtx.On非活性化();
				base.On活性化();
                if( !CDTXMania.bコンパクトモード )
                    this.tラベル名からステータスパネルを決定する( CDTXMania.stage選曲.r確定された曲.ar難易度ラベル[ CDTXMania.stage選曲.n確定された曲の難易度 ] );
			}
			finally
			{
				Trace.TraceInformation( "曲読み込みステージの活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void On非活性化()
		{
			Trace.TraceInformation( "曲読み込みステージを非活性化します。" );
			Trace.Indent();
            for (int i = 0; i < 8; i++)
            {
                this.st泡[i].ct進行 = null;
            }
            this.bSTAGEFILEが存在する = false;
            CDTXMania.t安全にDisposeする(ref this.ds背景動画);
			try
			{
				if( this.ftタイトル表示用フォント != null )
				{
					this.ftタイトル表示用フォント.Dispose();
					this.ftタイトル表示用フォント = null;
                    this.ftアーティスト名表示フォント.Dispose();
                    this.ftアーティスト名表示フォント = null;
				}
				base.On非活性化();
			}
			finally
			{
				Trace.TraceInformation( "曲読み込みステージの非活性化を完了しました。" );
				Trace.Unindent();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txベースパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\6_base panel.png"), false);
                this.txシンボル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\6_Symbol.png"), false);
                this.txLevel = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\6_LevelNumber.png"), false);
				this.tx背景 = CDTXMania.tテクスチャの生成( this.strSTAGEFILE, false );
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\6_difficulty panel.png"), false);
                this.txヘッダーパネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\6_header panel.png"), false);
                this.txDrumspeed = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_panel_icons.jpg"),false);
                this.txRISKY = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_panel_icons2.jpg"), false);
                this.txBall = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\6_Ball.png"));

					try
					{
						if( ( this.str曲タイトル != null ) && ( this.str曲タイトル.Length > 0 ) )
						{
							Bitmap image = new Bitmap( 1, 1 );
                            Bitmap image2 = new Bitmap( 1, 1 );
							Graphics graphics = Graphics.FromImage( image );
                            Graphics graphics2 = Graphics.FromImage( image2 ); 
							SizeF ef = graphics.MeasureString( this.str曲タイトル, this.ftタイトル表示用フォント );
                            SizeF ef2 = graphics.MeasureString( this.strアーティスト名, this.ftアーティスト名表示フォント);
							Size size = new Size( (int) Math.Ceiling( (double) ef.Width ), (int) Math.Ceiling( (double) ef.Height ) );
                            Size size2 = new Size( (int)Math.Ceiling( (double) ef2.Width), (int) Math.Ceiling( (double) ef2.Height) );
							graphics.Dispose();
							image.Dispose();
							image = new Bitmap( size.Width, size.Height );

                            if (string.IsNullOrEmpty(this.strアーティスト名))       //2012.02.11.kairera0467 アーティスト名が無かった場合の処理。
                                image2 = new Bitmap( size.Width, size.Height );
                            else
                                image2 = new Bitmap( size2.Width, size2.Height );

							graphics = Graphics.FromImage( image );
							graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            graphics2 = Graphics.FromImage(image2);
                            graphics2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

							graphics.DrawString( this.str曲タイトル, this.ftタイトル表示用フォント, Brushes.White, ( float ) 0f, ( float ) 0f );
                            graphics2.DrawString( this.strアーティスト名, this.ftアーティスト名表示フォント, Brushes.White, (float)0f, (float)0f);
							graphics.Dispose();
							this.txタイトル = new CTexture( CDTXMania.app.Device, image, CDTXMania.TextureFormat );
							this.txタイトル.vc拡大縮小倍率 = new Vector3( 0.42f, 0.5f, 1f );
                            this.txアーティスト = new CTexture( CDTXMania.app.Device, image2, CDTXMania.TextureFormat);
                            this.txアーティスト.vc拡大縮小倍率 = new Vector3(0.5f, 0.5f, 1f);
							image.Dispose();
						}
						else
						{
							this.txタイトル = null;
                            this.txアーティスト = null;
						}
					}
					catch( CTextureCreateFailedException )
					{
						Trace.TraceError( "テクスチャの生成に失敗しました。({0})", new object[] { this.strSTAGEFILE } );
						this.txタイトル = null;
                        this.txアーティスト = null;
						this.tx背景 = null;
					}
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                //テクスチャ11枚
				CDTXMania.tテクスチャの解放( ref this.tx背景 );
                CDTXMania.tテクスチャの解放( ref this.txヘッダーパネル );
                CDTXMania.tテクスチャの解放( ref this.txジャケット );
                CDTXMania.tテクスチャの解放( ref this.txベースパネル );
				CDTXMania.tテクスチャの解放( ref this.txタイトル );
                CDTXMania.tテクスチャの解放( ref this.txアーティスト );
                CDTXMania.tテクスチャの解放( ref this.txRISKY );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル);
                CDTXMania.tテクスチャの解放( ref this.txDrumspeed);
                CDTXMania.tテクスチャの解放( ref this.txLevel);
                CDTXMania.tテクスチャの解放( ref this.txシンボル );
                CDTXMania.tテクスチャの解放( ref this.txBall );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
        {
            string str;

            if (base.b活性化してない)
                return 0;

            #region [ 初めての進行描画 ]
            //-----------------------------
            if (base.b初めての進行描画)
            {
                Cスコア cスコア1 = CDTXMania.stage選曲.r確定されたスコア;
                if (this.sd読み込み音 != null)
                {
                    if (CDTXMania.Skin.sound曲読込開始音.b排他 && (CSkin.Cシステムサウンド.r最後に再生した排他システムサウンド != null))
                    {
                        CSkin.Cシステムサウンド.r最後に再生した排他システムサウンド.t停止する();
                    }
                    this.sd読み込み音.t再生を開始する();
                    this.nBGM再生開始時刻 = CSound管理.rc演奏用タイマ.n現在時刻;
                    this.nBGMの総再生時間ms = this.sd読み込み音.n総演奏時間ms;
                }
                else
                {
                    CDTXMania.Skin.sound曲読込開始音.t再生する();
                    this.nBGM再生開始時刻 = CSound管理.rc演奏用タイマ.n現在時刻;
                    this.nBGMの総再生時間ms = CDTXMania.Skin.sound曲読込開始音.n長さ・現在のサウンド;
                }
//				this.actFI.tフェードイン開始();							// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
                base.eフェーズID = CStage.Eフェーズ.共通_フェードイン;
                base.b初めての進行描画 = false;

                nWAVcount = 1;
                bitmapFilename = new Bitmap(1280, 720);
                graphicsFilename = Graphics.FromImage(bitmapFilename);
                graphicsFilename.TextRenderingHint = TextRenderingHint.AntiAlias;
                ftFilename = new Font("MS PGothic", 24f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            //-----------------------------
            #endregion

            #region [ ESC押下時は選曲画面に戻る ]
            if (tキー入力())
            {
                if (this.sd読み込み音 != null)
                {
                    this.sd読み込み音.tサウンドを停止する();
                    this.sd読み込み音.t解放する();
                }
                return (int)E曲読込画面の戻り値.読込中止;
            }
	        #endregion

            #region [ 背景、レベル、タイトル表示 ]
            //-----------------------------

            if( this.ds背景動画 != null && !this.bSTAGEFILEが存在する )
            {
                this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する( this.tx背景 );
                this.ds背景動画.t再生開始();
            }
            if( this.tx背景 != null )
            {
                if( this.ds背景動画 != null && this.ds背景動画.b上下反転 && !this.bSTAGEFILEが存在する )
                {
                    this.tx背景.t2D上下反転描画( CDTXMania.app.Device, 0, 0 );
                }
                else
                {
                    this.tx背景.t2D描画( CDTXMania.app.Device, 0, 0 );
                }
            }

                this.Start();
                for (int i = 0; i < 8; i++)
                {
                    if (this.st泡[i].b使用中)
                    {
                        this.st泡[i].n前回のValue = this.st泡[i].ct進行.n現在の値;
                        this.st泡[i].ct進行.t進行();
                        if (this.st泡[i].ct進行.b終了値に達した)
                        {
                            this.st泡[i].ct進行.t停止();
                            //this.st泡[i].b使用中 = false;
                        }
                        for (int n = this.st泡[i].n前回のValue; n < this.st泡[i].ct進行.n現在の値; n++)
                        {
                            this.st泡[i].fX += this.st泡[i].f加速度X;
                            this.st泡[i].fY -= this.st泡[i].f加速度Y;
                            this.st泡[i].f加速度X *= this.st泡[i].f加速度の加速度X;
                            this.st泡[i].f加速度Y *= this.st泡[i].f加速度の加速度Y;
                        }
                        Matrix mat = Matrix.Identity;

                        float x = (float)(this.st泡[i].ct進行.n現在の値 / 200.0f);
                        mat *= Matrix.Scaling(x, x, 1f);
                        mat *= Matrix.Translation(this.st泡[i].fX - SampleFramework.GameWindowSize.Width / 2, -(this.st泡[i].fY - SampleFramework.GameWindowSize.Height / 2), 0f);

                        if (this.txBall != null)
                        {
                            this.txBall.t3D描画(CDTXMania.app.Device, mat);
                            this.txBall.n透明度 = 200 - this.st泡[i].ct進行.n現在の値;
                        }
                    }

                }
            this.txシンボル.t2D描画(CDTXMania.app.Device, 422, 128);

            this.txベースパネル.t2D描画(CDTXMania.app.Device, 0, 0);
            this.txヘッダーパネル.t2D描画(CDTXMania.app.Device, 0, 0);
            this.tx難易度パネル.t2D描画(CDTXMania.app.Device, 268, 194 , new Rectangle(130 , (this.nIndex * 72), 130 , 72));


            string strDTXファイルパス = (CDTXMania.bコンパクトモード) ?
            CDTXMania.strコンパクトモードファイル : CDTXMania.stage選曲.r確定されたスコア.ファイル情報.ファイルの絶対パス;
            CDTX cdtx = new CDTX(strDTXファイルパス, true);
            
            //後に変数にしてギターベースなどでも正常に表示できるようにする予定
            STDGBVALUE<double> n表記するLEVEL = new STDGBVALUE<double>();
            n表記するLEVEL.Drums = cdtx.LEVEL.Drums / 10.0;
            n表記するLEVEL.Drums += ( cdtx.LEVELDEC.Drums != 0 ? cdtx.LEVELDEC.Drums / 100.0 : 0 );
            int DTXLevel = cdtx.LEVEL.Drums;
            double DTXLevelDeci = (DTXLevel * 10 - cdtx.LEVEL.Drums);

            string strLevel = string.Format( "{0:0.00}", n表記するLEVEL.Drums );

            if (cdtx.LEVEL.Drums > 100)
            {
                DTXLevel = cdtx.LEVEL.Drums / 100;
                DTXLevelDeci = (cdtx.LEVEL.Drums - (DTXLevel * 100));
            }
            else
            {
                DTXLevel = cdtx.LEVEL.Drums / 10;
                DTXLevelDeci = ( cdtx.LEVEL.Drums - DTXLevel * 10 );
            }

            if (CDTXMania.stage選曲.r確定されたスコア.譜面情報.b完全にCLASSIC譜面である.Drums && !cdtx.b強制的にXG譜面にする )
            {
                DTXLevel = cdtx.LEVEL.Drums;
                this.t大文字表示(338, 220, string.Format("{0,2:00}", DTXLevel));
                //this.txLevel.t2D描画(CDTXMania.app.Device, 359, 251, new Rectangle(145, 54, 7, 8));
            }
            else
            {
                //this.t大文字表示(335, 218, string.Format("{0:0}", DTXLevel));
                this.t大文字表示( 335, 218, string.Format( "{0:0}", strLevel.Substring(0, 1) ) );
                this.txLevel.t2D描画(CDTXMania.app.Device, 359, 251, new Rectangle(145, 54, 7, 8));
                if (cdtx.LEVEL.Drums > 100)
                {
                    this.t小文字表示(366, 238, string.Format("{0,2:00}", DTXLevelDeci));
                }
                else
                {
                    //this.t小文字表示(366, 238, string.Format("{0:0}", DTXLevelDeci));
                    this.t小文字表示( 354, 236, string.Format("{0:00}", strLevel.Substring(1, 3) ) );
                    //this.txLevel.t2D描画(CDTXMania.app.Device, 378, 238, new Rectangle(13, 40, 13, 22));
                }
            }
            if( CDTXMania.ConfigIni.bSkillModeを自動切換えする == true && CDTXMania.ConfigIni.bDrums有効 )
            {
                if( CDTXMania.stage選曲.r確定されたスコア.譜面情報.b完全にCLASSIC譜面である.Drums && !cdtx.b強制的にXG譜面にする )
                {
                    CDTXMania.ConfigIni.nSkillMode = 0;
                }
                else
                {
                    CDTXMania.ConfigIni.nSkillMode = 1;
                }
            }

            string path = cdtx.strフォルダ名 + cdtx.PREIMAGE;
                if (!File.Exists(path))
                {
                    //Trace.TraceWarning("ファイルが存在しません。({0})", new object[] { path });
                    this.txジャケット = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\5_preimage default.png"));
                }
                else
                {
                    this.txジャケット = CDTXMania.tテクスチャの生成(path);
                }
                int y = 184;
                if (this.txタイトル != null)
                {
                    this.txタイトル.t2D描画(CDTXMania.app.Device, (int)(510 + (this.txタイトル.vc拡大縮小倍率.X)), y);
                    this.txアーティスト.t2D描画(CDTXMania.app.Device, (int)(792 - (this.txアーティスト.sz画像サイズ.Width * this.txアーティスト.vc拡大縮小倍率.X)), 505);
                }
                //this.txジャケット.vc拡大縮小倍率.X = 0.689f;
                //this.txジャケット.vc拡大縮小倍率.Y = 0.699f;
                this.txジャケット.vc拡大縮小倍率.X = 280.0f / this.txジャケット.sz画像サイズ.Width;
                this.txジャケット.vc拡大縮小倍率.Y = 280.0f / this.txジャケット.sz画像サイズ.Height;
                this.txジャケット.t2D描画(CDTXMania.app.Device, 508, 218);

                this.txジャケット.Dispose();

                this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                this.nCurrentRISKY = CDTXMania.ConfigIni.nRisky;
                if (CDTXMania.ConfigIni.nRisky > 10)
                {
                    nCurrentRISKY = 0;
                }
                this.txDrumspeed.vc拡大縮小倍率 = new Vector3(36.0f / 42.0f, 36.0f / 48.0f, 1.0f);
                this.txRISKY.vc拡大縮小倍率 = new Vector3(36.0f / 42.0f, 36.0f / 48.0f, 1.0f);
                this.txDrumspeed.t2D描画(CDTXMania.app.Device, 288, 298 , new Rectangle(0, 0 + (nCurrentDrumspeed * 48), 42, 48));
                this.txRISKY.t2D描画(CDTXMania.app.Device, 288, 346, new Rectangle(0, 0 + (nCurrentRISKY * 48), 42, 48));
            //-----------------------------
            #endregion

            switch (base.eフェーズID)
            {
                case CStage.Eフェーズ.共通_フェードイン:
                    //if( this.actFI.On進行描画() != 0 )					// #27787 2012.3.10 yyagi 曲読み込み画面のフェードインの省略
                    // 必ず一度「CStaeg.Eフェーズ.共通_フェードイン」フェーズを経由させること。
                    // さもないと、曲読み込みが完了するまで、曲読み込み画面が描画されない。
                    base.eフェーズID = CStage.Eフェーズ.NOWLOADING_DTXファイルを読み込む;
                    return (int)E曲読込画面の戻り値.継続;

                case CStage.Eフェーズ.NOWLOADING_DTXファイルを読み込む:
                    {
                        timeBeginLoad = DateTime.Now;
                        TimeSpan span;
                        str = null;
                        if (!CDTXMania.bコンパクトモード)
                            str = CDTXMania.stage選曲.r確定されたスコア.ファイル情報.ファイルの絶対パス;
                        else
                            str = CDTXMania.strコンパクトモードファイル;

                        CScoreIni ini = new CScoreIni(str + ".score.ini");
                        ini.t全演奏記録セクションの整合性をチェックし不整合があればリセットする();

                        if ((CDTXMania.DTX != null) && CDTXMania.DTX.b活性化してる)
                            CDTXMania.DTX.On非活性化();

                        CDTXMania.DTX = new CDTX(str, false, ((double)CDTXMania.ConfigIni.n演奏速度) / 20.0, ini.stファイル.BGMAdjust);
                        Trace.TraceInformation("----曲情報-----------------");
                        Trace.TraceInformation("TITLE: {0}", CDTXMania.DTX.TITLE);
                        Trace.TraceInformation("FILE: {0}",  CDTXMania.DTX.strファイル名の絶対パス);
                        Trace.TraceInformation("---------------------------");

                        span = (TimeSpan)(DateTime.Now - timeBeginLoad);
                        Trace.TraceInformation("DTX読込所要時間:           {0}", span.ToString());

                        if (CDTXMania.bコンパクトモード)
                            CDTXMania.DTX.MIDIレベル = 1;
                        else
                            CDTXMania.DTX.MIDIレベル = (CDTXMania.stage選曲.r確定された曲.eノード種別 == C曲リストノード.Eノード種別.SCORE_MIDI) ? CDTXMania.stage選曲.n現在選択中の曲の難易度 : 0;

                        base.eフェーズID = CStage.Eフェーズ.NOWLOADING_WAVファイルを読み込む;
                        timeBeginLoadWAV = DateTime.Now;
                        return (int)E曲読込画面の戻り値.継続;
                    }

                case CStage.Eフェーズ.NOWLOADING_WAVファイルを読み込む:
                    {
                        //if (nWAVcount == 1 && CDTXMania.DTX.listWAV.Count > 0)			// #28934 2012.7.7 yyagi (added checking Count)
                        {
                            //ShowProgressByFilename(CDTXMania.DTX.listWAV[nWAVcount].strファイル名);
                        }
                        int looptime = (CDTXMania.ConfigIni.b垂直帰線待ちを行う) ? 3 : 1;	// VSyncWait=ON時は1frame(1/60s)あたり3つ読むようにする
                        for (int i = 0; i < looptime && nWAVcount <= CDTXMania.DTX.listWAV.Count; i++)
                        {
                            if (CDTXMania.DTX.listWAV[nWAVcount].listこのWAVを使用するチャンネル番号の集合.Count > 0)	// #28674 2012.5.8 yyagi
                            {
                                CDTXMania.DTX.tWAVの読み込み(CDTXMania.DTX.listWAV[nWAVcount]);
                            }
                            nWAVcount++;
                        }
                        //if (nWAVcount <= CDTXMania.DTX.listWAV.Count)
                        {
                            //ShowProgressByFilename(CDTXMania.DTX.listWAV[nWAVcount].strファイル名);
                        }
                        if (nWAVcount > CDTXMania.DTX.listWAV.Count)
                        {
                            TimeSpan span = (TimeSpan)(DateTime.Now - timeBeginLoadWAV);
                            Trace.TraceInformation("WAV読込所要時間({0,4}):     {1}", CDTXMania.DTX.listWAV.Count, span.ToString());
                            timeBeginLoadWAV = DateTime.Now;

							if ( CDTXMania.ConfigIni.bDynamicBassMixerManagement )
							{
								CDTXMania.DTX.PlanToAddMixerChannel();
							}
                            CDTXMania.DTX.t譜面仕様変更( E楽器パート.DRUMS, CDTXMania.ConfigIni.eNumOfLanes.Drums );
                            CDTXMania.DTX.t旧仕様のドコドコチップを振り分ける(E楽器パート.DRUMS, CDTXMania.ConfigIni.bAssignToLBD.Drums);
                            CDTXMania.DTX.tドコドコ仕様変更(E楽器パート.DRUMS, CDTXMania.ConfigIni.eDkdkType.Drums);
                            CDTXMania.DTX.tドラムのミラー化(E楽器パート.DRUMS, CDTXMania.ConfigIni.eMirror.Drums);
                            CDTXMania.DTX.tドラムのランダム化(E楽器パート.DRUMS, CDTXMania.ConfigIni.eRandom.Drums);
                            CDTXMania.DTX.tドラムの足ランダム化(E楽器パート.DRUMS, CDTXMania.ConfigIni.eRandomPedal.Drums);
                            CDTXMania.DTX.tギターとベースのランダム化(E楽器パート.GUITAR, CDTXMania.ConfigIni.eRandom.Guitar);
                            CDTXMania.DTX.tギターとベースのランダム化(E楽器パート.BASS, CDTXMania.ConfigIni.eRandom.Bass);

                            if (CDTXMania.ConfigIni.bギタレボモード)
                                CDTXMania.stage演奏ギター画面.On活性化();
                            else
                                CDTXMania.stage演奏ドラム画面.On活性化();

							span = (TimeSpan) ( DateTime.Now - timeBeginLoadWAV );
							Trace.TraceInformation( "WAV/譜面後処理時間({0,4}):  {1}", ( CDTXMania.DTX.listBMP.Count + CDTXMania.DTX.listBMPTEX.Count + CDTXMania.DTX.listAVI.Count ), span.ToString() );

							base.eフェーズID = CStage.Eフェーズ.NOWLOADING_BMPファイルを読み込む;
                        }
                        return (int)E曲読込画面の戻り値.継続;
                    }

				case CStage.Eフェーズ.NOWLOADING_BMPファイルを読み込む:
					{
						TimeSpan span;
						DateTime timeBeginLoadBMPAVI = DateTime.Now;
						if ( CDTXMania.ConfigIni.bBGA有効 )
							CDTXMania.DTX.tBMP_BMPTEXの読み込み();

						if ( CDTXMania.ConfigIni.bAVI有効 )
							CDTXMania.DTX.tAVIの読み込み();
						span = ( TimeSpan ) ( DateTime.Now - timeBeginLoadBMPAVI );
						Trace.TraceInformation( "BMP/AVI読込所要時間({0,4}): {1}", ( CDTXMania.DTX.listBMP.Count + CDTXMania.DTX.listBMPTEX.Count + CDTXMania.DTX.listAVI.Count ), span.ToString() );

						span = ( TimeSpan ) ( DateTime.Now - timeBeginLoad );
						Trace.TraceInformation( "総読込時間:                {0}", span.ToString() );

						//if ( bitmapFilename != null )
						{
						//	bitmapFilename.Dispose();
						//	bitmapFilename = null;
						}
						//if ( graphicsFilename != null )
						{
						//	graphicsFilename.Dispose();
						//	graphicsFilename = null;
						}
						//if ( ftFilename != null )
						{
						//	ftFilename.Dispose();
						//	ftFilename = null;
						}
						CDTXMania.Timer.t更新();
						base.eフェーズID = CStage.Eフェーズ.NOWLOADING_システムサウンドBGMの完了を待つ;
                        return (int)E曲読込画面の戻り値.継続;
					}

                case CStage.Eフェーズ.NOWLOADING_システムサウンドBGMの完了を待つ:
                    {
                        long nCurrentTime = CDTXMania.Timer.n現在時刻;
                        if (nCurrentTime < this.nBGM再生開始時刻)
                            this.nBGM再生開始時刻 = nCurrentTime;

                        //						if ( ( nCurrentTime - this.nBGM再生開始時刻 ) > ( this.nBGMの総再生時間ms - 1000 ) )
                        if ((nCurrentTime - this.nBGM再生開始時刻) > (this.nBGMの総再生時間ms))	// #27787 2012.3.10 yyagi 1000ms == フェードイン分の時間
                        {
                            this.actFO.tフェードアウト開始();
                            base.eフェーズID = CStage.Eフェーズ.共通_フェードアウト;
                        }
                        return (int)E曲読込画面の戻り値.継続;
                    }

                case CStage.Eフェーズ.共通_フェードアウト:
                    if (this.actFO.On進行描画() == 0)
                        return 0;
                    //if (txFilename != null)
                    {
                    //    txFilename.Dispose();
                    }
                    if (this.sd読み込み音 != null)
                    {
                        this.sd読み込み音.t解放する();
                    }
                    return (int)E曲読込画面の戻り値.読込完了;
            }
            return (int)E曲読込画面の戻り値.継続;
        }


        /// <summary>
		/// ESC押下時、trueを返す
		/// </summary>
		/// <returns></returns>
		protected bool tキー入力()
		{
			IInputDevice keyboard = CDTXMania.Input管理.Keyboard;
			if 	( keyboard.bキーが押された( (int) SlimDX.DirectInput.Key.Escape ) )		// escape (exit)
			{
                if (CDTXMania.ConfigIni.bギタレボモード)
                {
                    if (CDTXMania.stage演奏ギター画面.b活性化してる == true)
                        CDTXMania.stage演奏ギター画面.On非活性化();
                }
                else
                {
                    if(CDTXMania.stage演奏ドラム画面.b活性化してる == true)
                        CDTXMania.stage演奏ドラム画面.On非活性化();
                }
                
				return true;
			}
			return false;
		}


		private void ShowProgressByFilename(string strファイル名 )
        {
            #region[ 廃止。 ]
            /*
            if ( graphicsFilename != null && ftFilename != null )
			{
				graphicsFilename.Clear( Color.Transparent );
				graphicsFilename.DrawString( strファイル名, ftFilename, Brushes.White, new RectangleF( 0, 0, 720, 24 ) );
				if ( txFilename != null )
				{
					txFilename.Dispose();
				}
				txFilename = new CTexture( CDTXMania.app.Device, bitmapFilename, CDTXMania.TextureFormat );
				txFilename.vc拡大縮小倍率 = new Vector3( 0.5f, 0.5f, 1f );
				txFilename.t2D描画( CDTXMania.app.Device, 0, 720 - 16 );
            }
            */
            #endregion
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
        [StructLayout(LayoutKind.Sequential)]
        private struct ST泡
        {
            public bool b使用中;
            public CCounter ct進行;
            public int n前回のValue;
            public float fX;
            public float fY;
            public float f加速度X;
            public float f加速度Y;
            public float f加速度の加速度X;
            public float f加速度の加速度Y;
            public float f半径;
        }
//		private CActFIFOBlack actFI;
		private CActFIFOBlackStart actFO;

        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;
		private Font ftタイトル表示用フォント;
        private Font ftアーティスト名表示フォント;
        private bool bSTAGEFILEが存在する;
		private long nBGMの総再生時間ms;
		private long nBGM再生開始時刻;
		private CSound sd読み込み音;
		private string strSTAGEFILE;
		private string str曲タイトル;
        private string strアーティスト名;
		private CTexture txタイトル;
        private CTexture txアーティスト;
        private CTexture txベースパネル;
        private CTexture txヘッダーパネル;
        private CTexture tx難易度パネル;
        private CTexture txジャケット;
		private CTexture tx背景;
        private CTexture txDrumspeed;
        private CTexture txRISKY;
        private CTexture txシンボル;
        private ST泡[] st泡 = new ST泡[8];
        private CDirectShow ds背景動画;

		private DateTime timeBeginLoad;
		private DateTime timeBeginLoadWAV;
		private int nWAVcount;
        private int nCurrentDrumspeed;
        private int nCurrentRISKY;
//		private CTexture txFilename;
        private CTexture txLevel;
        private CTexture txBall;

		private Bitmap bitmapFilename;
		private Graphics graphicsFilename;
		private Font ftFilename;

        [StructLayout(LayoutKind.Sequential)]
        public struct STATUSPANEL
        {
            public string label;
            public int status;
        }
        public int nIndex;
        public STATUSPANEL[] stパネルマップ;
		//-----------------

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
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 13, 22);
                        if (this.txLevel != null)
                        {
                            this.txLevel.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += 12;
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
                        Rectangle rc画像内の描画領域 = new Rectangle(this.st大文字位置[j].pt.X, this.st大文字位置[j].pt.Y, 22, 40);
                        if (c == '.')
                        {
                            rc画像内の描画領域.Width -= 2;
                            rc画像内の描画領域.Height -= 2;
                        }
                        if (this.txLevel != null)
                        {
                            this.txLevel.t2D描画(CDTXMania.app.Device, x, y, rc画像内の描画領域);
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
                    x += 24;
                }
            }
        }
        #endregion
    }
}
