using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CActSelectステータスパネル : CActivity
	{
		// メソッド

		public CActSelectステータスパネル()
		{
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
            st文字位置22.pt = new Point(0, 40);
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
			base.b活性化してない = true;
		}
		public void t選択曲が変更された()
		{
			C曲リストノード c曲リストノード = CDTXMania.stage選曲.r現在選択中の曲;
			Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;
			if( ( c曲リストノード != null ) && ( cスコア != null ) )
			{
				this.n現在選択中の曲の難易度 = CDTXMania.stage選曲.n現在選択中の曲の難易度;
				for( int i = 0; i < 3; i++ )
				{
                    /*
					int nLevel = cスコア.譜面情報.レベル[ i ];
					if( nLevel < 0 )
					{
						nLevel = 0;
					}
					if( nLevel > 999 )
					{
						nLevel = 999;
					}
					this.n現在選択中の曲のレベル[ i ] = nLevel;
                     */
                    
					this.n現在選択中の曲の最高ランク[ i ] = cスコア.譜面情報.最大ランク[ i ];
					this.b現在選択中の曲がフルコンボ[ i ] = cスコア.譜面情報.フルコンボ[ i ];
					this.db現在選択中の曲の最高スキル値[ i ] = cスコア.譜面情報.最大スキル[ i ];
                    this.db現在選択中の曲の曲別スキル[i] = cスコア.譜面情報.最大曲別スキル[i];
                    for (int j = 0; j < 5; j++)
                    {
                        if (c曲リストノード.arスコア[j] != null)
                        {
                            this.n現在選択中の曲の最高ランク難易度毎[j][i] = c曲リストノード.arスコア[j].譜面情報.最大ランク[i];
                            this.b現在選択中の曲がフルコンボ難易度毎[j][i] = c曲リストノード.arスコア[j].譜面情報.フルコンボ[i];
                        }
                    }
				}
				for( int i = 0; i < 5; i++ )
				{
                    if (c曲リストノード.arスコア[ i ] != null)
                    {
                        int nLevel = c曲リストノード.arスコア[i].譜面情報.レベル.Drums;
                        if (nLevel < 0)
                        {
                            nLevel = 0;
                        }
                        if (nLevel > 999)
                        {
                            nLevel = 999;
                        }
                        this.n選択中の曲のレベル難易度毎[i] = nLevel;
                        this.db現在選択中の曲の最高スキル値難易度毎[ i ] = c曲リストノード.arスコア[ i ].譜面情報.最大スキル.Drums;

                    }
                    else
                    {
                        this.n選択中の曲のレベル難易度毎[i] = 0;
                    }
                    this.str難易度ラベル[i] = c曲リストノード.ar難易度ラベル[i];

				}
				if( this.r直前の曲 != c曲リストノード )
				{
					this.n難易度開始文字位置 = 0;
				}
				this.r直前の曲 = c曲リストノード;
			}
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n本体X = 6;
			this.n本体Y = 0x20b;
			this.n現在選択中の曲の難易度 = 0;
			for( int i = 0; i < 3; i++ )
			{
				this.n現在選択中の曲のレベル[ i ] = 0;
                this.n現在選択中の曲のBPM = 0;
                this.db現在選択中の曲の曲別スキル[i] = 0.0;
				this.n現在選択中の曲の最高ランク[ i ] = (int)CScoreIni.ERANK.UNKNOWN;
				this.b現在選択中の曲がフルコンボ[ i ] = false;
				this.db現在選択中の曲の最高スキル値[ i ] = 0.0;
                for (int j = 0; j < 5; j++)
                {
                    this.n現在選択中の曲の最高ランク難易度毎[j][i] = (int)CScoreIni.ERANK.UNKNOWN;
                    this.b現在選択中の曲がフルコンボ難易度毎[j][i] = false;
                }
			}
			for( int j = 0; j < 5; j++ )
			{
				this.str難易度ラベル[ j ] = "";
                this.n選択中の曲のレベル難易度毎[ j ] = 0;
                this.db現在選択中の曲の最高スキル値難易度毎[j] = 0.0;
			}
			this.n難易度開始文字位置 = 0;
			this.r直前の曲 = null;
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ct登場アニメ用 = null;
			this.ct難易度スクロール用 = null;
			this.ct難易度矢印用 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.txパネル本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_status panel.png" ));
				this.txレベル数字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenSelect level numbers.png" ), false );
				this.txスキルゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenSelect skill gauge.png" ), false );
				this.txゲージ用数字他 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_skill icon.png" ), false );
				this.tx難易度用矢印 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenSelect triangle arrow.png" ), false );
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty panel.png"));
                this.tx難易度数字XG = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\6_LevelNumber.png"));
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txレベル数字 );
				CDTXMania.tテクスチャの解放( ref this.txスキルゲージ );
				CDTXMania.tテクスチャの解放( ref this.txゲージ用数字他 );
				CDTXMania.tテクスチャの解放( ref this.tx難易度用矢印 );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル );
                CDTXMania.tテクスチャの解放( ref this.tx難易度数字XG );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				#region [ 初めての進行描画 ]
				//-----------------
				if( base.b初めての進行描画 )
				{
					this.ct登場アニメ用 = new CCounter( 0, 100, 5, CDTXMania.Timer );
					this.ct難易度スクロール用 = new CCounter( 0, 20, 1, CDTXMania.Timer );
					this.ct難易度矢印用 = new CCounter( 0, 5, 80, CDTXMania.Timer );
					base.b初めての進行描画 = false;
				}
				//-----------------
				#endregion

				// 進行

				this.ct登場アニメ用.t進行();

				this.ct難易度スクロール用.t進行();
				if( this.ct難易度スクロール用.b終了値に達した )
				{
					int num = this.n現在の難易度ラベルが完全表示されているかを調べてスクロール方向を返す();
					if( num < 0 )
					{
						this.n難易度開始文字位置--;
					}
					else if( num > 0 )
					{
						this.n難易度開始文字位置++;
					}
					this.ct難易度スクロール用.n現在の値 = 0;
				}
	
				this.ct難易度矢印用.t進行Loop();
				
				// 描画

				#region [ パネル本体の描画 ]
				//-----------------
				if( this.txパネル本体 != null )
				{
					if( this.ct登場アニメ用.b終了値に達した )
					{
						this.n本体X = 0;
						this.n本体Y = 0;
					}
					else
					{
						double num2 = ( (double) ( 100 - this.ct登場アニメ用.n現在の値 ) ) / 100.0;
						double num3 = Math.Sin( Math.PI / 2 * num2 );
						this.n本体X = 0 - ( (int) ( ( this.txパネル本体.sz画像サイズ.Width * num3 ) * num3 ) );
						this.n本体Y = 0;
					}
					this.txパネル本体.t2D描画( CDTXMania.app.Device, this.n本体X, this.n本体Y );
				}
				//-----------------
				#endregion

                #region [ 難易度パネルの描画 ]
                //-----------------
                int[] y差分 = new int[5];
                for ( int i = 0; i < 5; i++ )
                {
                    if (this.n現在選択中の曲の難易度 == i)
                    {
                        y差分[i] += 10;
                    }
                }
                if ( this.tx難易度パネル != null )
                {
                    if(this.str難易度ラベル[0] != null)
                        this.tx難易度パネル.t2D描画(CDTXMania.app.Device, 346 + this.n本体X, 43 - y差分[0], new Rectangle(0, 0, 132, 98));
                    if(this.str難易度ラベル[1] != null)
                        this.tx難易度パネル.t2D描画(CDTXMania.app.Device, 489 + this.n本体X, 43 - y差分[1], new Rectangle(0, 98, 132, 98));
                    if(this.str難易度ラベル[2] != null)
                        this.tx難易度パネル.t2D描画(CDTXMania.app.Device, 632 + this.n本体X, 43 - y差分[2], new Rectangle(0, 196, 132, 98));
                    if(this.str難易度ラベル[3] != null)
                        this.tx難易度パネル.t2D描画(CDTXMania.app.Device, 775 + this.n本体X, 43 - y差分[3], new Rectangle(0, 294, 132, 98));
                    if(this.str難易度ラベル[4] != null)
                        this.tx難易度パネル.t2D描画(CDTXMania.app.Device, 918 + this.n本体X, 43 - y差分[4], new Rectangle(0, 392, 132, 98));
                }
                //-----------------
                #endregion

                #region [ 難易度文字列の描画 ]
                //-----------------
				for( int i = 0; i < 5; i++ )
				{
                    CDTXMania.act文字コンソール.tPrint(346 + (i * 142), 8, (this.n現在選択中の曲の難易度 == i) ? C文字コンソール.Eフォント種別.赤 : C文字コンソール.Eフォント種別.白, this.str難易度ラベル[i]);
				}
				//-----------------
				#endregion

				Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;

				#region [ 選択曲の Lv の描画 ]
				//-----------------
				if( ( cスコア != null ) && ( this.tx難易度数字XG != null ) )
				{
					for( int i = 0; i < 5; i++ )
					{
                        int[] n難易度整数 = new int[5];
                        int[] n難易度小数 = new int[5];
                        if(n選択中の曲のレベル難易度毎[ i ] > 100)
                        {
                            n難易度整数[i] = (int)this.n選択中の曲のレベル難易度毎[i] / 100;
                            n難易度小数[i] = (n選択中の曲のレベル難易度毎[i] - (n難易度整数[i] * 100));
                        }
                        else if(n選択中の曲のレベル難易度毎[i] < 100)
                        {
                            n難易度整数[i] = (int)this.n選択中の曲のレベル難易度毎[i] / 10;
                            n難易度小数[i] = (n選択中の曲のレベル難易度毎[i] - (n難易度整数[i] * 10)) * 10;
                        }

                        if (this.str難易度ラベル[i] != null)
                        {
                            this.t大文字表示(419 + (i * 143), 62 - y差分[i], string.Format("{0:0}", n難易度整数[i]));
                            this.t小文字表示(448 + (i * 143), 80 - y差分[i], string.Format("{0,2:00}", n難易度小数[i]));
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 440 + (i * 143), 94 - y差分[i], new Rectangle(145, 54, 7, 8));
                        }
					}
				}

                if (CDTXMania.stage選曲.r現在選択中の曲 != null)
                {
                    switch (CDTXMania.stage選曲.r現在選択中の曲.eノード種別)
                    {
                        case C曲リストノード.Eノード種別.SCORE:
                            {
                                this.n現在選択中の曲のBPM = cスコア.譜面情報.Bpm;
                                break;
                            }
                        default:
                            {
                                this.n現在選択中の曲のBPM = 0;
                                break;
                            }
                    }

                    CDTXMania.act文字コンソール.tPrint(420, 580, C文字コンソール.Eフォント種別.白, string.Format("BPM:{0:####0}", this.n現在選択中の曲のBPM));
                }



				//-----------------
				#endregion
				#region [ 選択曲の 最高スキル値ゲージ＋数値の描画 ]
				//-----------------
				for( int i = 0; i < 5; i++ )
				{
					if ( this.str難易度ラベル[i] != null && this.db現在選択中の曲の最高スキル値難易度毎[ i ] != 0.00 )
					{
                        this.t達成率表示(429 + (i * 143), 120 - y差分[i], string.Format("{0,6:##0.00}%", this.db現在選択中の曲の最高スキル値難易度毎[i]));
					}
				}
				//-----------------
				#endregion
				#region [ 選択曲の 最高ランクの描画 ]
				//-----------------
				for( int i = 0; i < 5; i++ )
				{
					int nMaxRank = this.n現在選択中の曲の最高ランク難易度毎[ i ].Drums;
					if( nMaxRank != 99 )
					{
						if ( nMaxRank < 0 )
						{
							nMaxRank = 0;
						}
						if( nMaxRank > 6 )
						{
							nMaxRank = 6;
						}
						if( this.txゲージ用数字他 != null && this.str難易度ラベル[ i ] != null)
						{
							this.txゲージ用数字他.t2D描画( CDTXMania.app.Device, (353 + ( i * 143 )), 104 - y差分[ i ], new Rectangle(42 + nMaxRank * 32 , 0 , 32, 32) );
						}
					}
				}
				//-----------------
				#endregion
				#region [ 選択曲の FullCombo の 描画 ]
				//-----------------
				//Rectangle rectFullCombo = new Rectangle( 60, 0x30, 60, 0x18 );
				for( int i = 0; i < 5; i++ )
				{
					if( this.b現在選択中の曲がフルコンボ難易度毎[ i ].Drums )
					{
						//int[ , ] nDispPosYOffset = { { 0, 0x20, 0x3f }, { 0, 0x3f, 0x20 } };
						int x = 386 + ( i  * 143);
						if( this.txゲージ用数字他 != null )
						{
							this.txゲージ用数字他.t2D描画( CDTXMania.app.Device, x, 106 - y差分[ i ], new Rectangle(0, 0, 42, 32) );
						}
					}
				}
				//-----------------
				#endregion
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct ST数字
		{
			public char ch;
			public Rectangle rc;
			public ST数字( char ch, Rectangle rc )
			{
				this.ch = ch;
				this.rc = rc;
			}
		}
        [StructLayout(LayoutKind.Sequential)]
        private struct ST達成率数字
        {
            public char ch;
            public Rectangle rc;
            public ST達成率数字(char ch, Rectangle rc)
            {
                this.ch = ch;
                this.rc = rc;
            }
        }
        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;

		private STDGBVALUE<bool> b現在選択中の曲がフルコンボ;
        private STDGBVALUE<bool>[] b現在選択中の曲がフルコンボ難易度毎 = new STDGBVALUE<bool>[5];
		private CCounter ct登場アニメ用;
		private CCounter ct難易度スクロール用;
		private CCounter ct難易度矢印用;
		private STDGBVALUE<double> db現在選択中の曲の最高スキル値;
        private double[] db現在選択中の曲の最高スキル値難易度毎 = new double[5];
        private STDGBVALUE<double> db現在選択中の曲の曲別スキル;
		private STDGBVALUE<int> n現在選択中の曲のレベル;
        private int[] n選択中の曲のレベル難易度毎 = new int[5];
        private double n現在選択中の曲のBPM;
		private STDGBVALUE<int> n現在選択中の曲の最高ランク;
        private STDGBVALUE<int>[] n現在選択中の曲の最高ランク難易度毎 = new STDGBVALUE<int>[5];
		private int n現在選択中の曲の難易度;
		private int n難易度開始文字位置;
		private const int n難易度表示可能文字数 = 0x24;
		private int n本体X;
		private int n本体Y;
        private readonly Rectangle[] rcランク = new Rectangle[] { new Rectangle(0, 0x30, 20, 15), new Rectangle(20, 0x30, 20, 15), new Rectangle(40, 0x30, 20, 15), new Rectangle(0, 0x3f, 20, 15), new Rectangle(20, 0x3f, 20, 15), new Rectangle(40, 0x3f, 20, 15), new Rectangle(0, 0x4e, 20, 15) };
        private readonly Rectangle[] rc数字 = new Rectangle[] { new Rectangle(0, 0, 15, 0x13), new Rectangle(15, 0, 15, 0x13), new Rectangle(30, 0, 15, 0x13), new Rectangle(0x2d, 0, 15, 0x13), new Rectangle(0, 0x13, 15, 0x13), new Rectangle(15, 0x13, 15, 0x13), new Rectangle(30, 0x13, 15, 0x13), new Rectangle(0x2d, 0x13, 15, 0x13), new Rectangle(0, 0x26, 15, 0x13), new Rectangle(15, 0x26, 15, 0x13), new Rectangle(30, 0x26, 15, 0x13), new Rectangle(0x2d, 0x26, 15, 0x13) };
		private C曲リストノード r直前の曲;
		public string[] str難易度ラベル = new string[] { "", "", "", "", "" };
        private readonly ST数字[] st数字 = new ST数字[] { new ST数字('0', new Rectangle(0, 0, 0x10, 0x11)), new ST数字('1', new Rectangle(0x10, 0, 0x10, 0x11)), new ST数字('2', new Rectangle(0x20, 0, 0x10, 0x11)), new ST数字('3', new Rectangle(0x30, 0, 0x10, 0x11)), new ST数字('4', new Rectangle(0x40, 0, 0x10, 0x11)), new ST数字('5', new Rectangle(80, 0, 0x10, 0x11)), new ST数字('6', new Rectangle(0, 0x11, 0x10, 0x10)), new ST数字('7', new Rectangle(0x10, 0x11, 0x10, 0x10)), new ST数字('8', new Rectangle(0x20, 0x11, 0x10, 0x10)), new ST数字('9', new Rectangle(0x30, 0x11, 0x10, 0x10)), new ST数字('.', new Rectangle(0x40, 0x11, 8, 0x10)), new ST数字('p', new Rectangle(0x48, 0x11, 30, 0x10)) };
        private readonly ST達成率数字[] st達成率数字 = new ST達成率数字[] { new ST達成率数字('0', new Rectangle(0, 62, 7, 16)), new ST達成率数字('1', new Rectangle(7, 62, 7, 16)), new ST達成率数字('2', new Rectangle(14, 62, 7, 16)), new ST達成率数字('3', new Rectangle(21, 62, 7, 16)), new ST達成率数字('4', new Rectangle(28, 62, 7, 16)), new ST達成率数字('5', new Rectangle(35, 62, 7, 16)), new ST達成率数字('6', new Rectangle(42, 62, 7, 16)), new ST達成率数字('7', new Rectangle(49, 62, 7, 16)), new ST達成率数字('8', new Rectangle(56, 62, 7, 16)), new ST達成率数字('9', new Rectangle(63, 62, 7, 16)), new ST達成率数字('%', new Rectangle(70, 62, 9, 16)), new ST達成率数字('.', new Rectangle(79, 62, 3, 16)) };
        private readonly Rectangle rcunused = new Rectangle(0, 0x21, 80, 15);
		private CTexture txゲージ用数字他;
		private CTexture txスキルゲージ;
		private CTexture txパネル本体;
		private CTexture txレベル数字;
		private CTexture tx難易度用矢印;
        private CTexture tx難易度パネル;
        private CTexture tx難易度数字XG;
        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
        }
        private int n現在の難易度ラベルが完全表示されているかを調べてスクロール方向を返す()
        {
            int num = 0;
            int length = 0;
            for (int i = 0; i < 5; i++)
            {
                if ((this.str難易度ラベル[i] != null) && (this.str難易度ラベル[i].Length > 0))
                {
                    length = this.str難易度ラベル[i].Length;
                }
                if (this.n現在選択中の曲の難易度 == i)
                {
                    break;
                }
                if ((this.str難易度ラベル[i] != null) && (this.str難易度ラベル.Length > 0))
                {
                    num += length + 2;
                }
            }
            if (num >= (this.n難易度開始文字位置 + 0x24))
            {
                return 1;
            }
            if ((num + length) <= this.n難易度開始文字位置)
            {
                return -1;
            }
            if (((num + length) - 1) >= (this.n難易度開始文字位置 + 0x24))
            {
                return 1;
            }
            if (num < this.n難易度開始文字位置)
            {
                return -1;
            }
            return 0;
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
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 13, 22);
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rectangle);
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
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rc画像内の描画領域);
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
        private void t達成率表示(int x, int y, string str)
        {
            for (int j = 0; j < str.Length; j++)
            {
                char c = str[j];
                for (int i = 0; i < this.st達成率数字.Length; i++)
                {
                    if (this.st達成率数字[i].ch == c)
                    {
                        Rectangle rectangle = new Rectangle(this.st達成率数字[i].rc.X, this.st達成率数字[i].rc.Y, 7, 16);
                        if (c == '.')
                        {
                            rectangle.Width -= 2;
                        }
                        if (this.tx難易度数字XG != null)
                        {
                            this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                if (c == '.')
                {
                    x += 4;
                }
                else
                {
                    x += 7;
                }
            }
        }
		//-----------------
		#endregion
	}
}
