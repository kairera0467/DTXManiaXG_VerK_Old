using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
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
                    if(CDTXMania.ConfigIni.nSkillMode == 0)
                    {
                        this.n現在選択中の曲の最高ランク[ i ] = cスコア.譜面情報.最大ランク[ i ];
                    }
                    else if(CDTXMania.ConfigIni.nSkillMode == 1)
                    {
                        this.n現在選択中の曲の最高ランク[ i ] = DTXMania.CScoreIni.tランク値を計算して返す( 0, cスコア.譜面情報.最大スキル[ i ] );
                    }
					this.b現在選択中の曲がフルコンボ[ i ] = cスコア.譜面情報.フルコンボ[ i ];
					this.db現在選択中の曲の最高スキル値[ i ] = cスコア.譜面情報.最大スキル[ i ];
                    this.db現在選択中の曲の曲別スキル[i] = cスコア.譜面情報.最大曲別スキル[i];
                    this.b現在選択中の曲の譜面[i] = cスコア.譜面情報.b譜面がある[i];
                    this.n現在選択中の曲のレベル[i] = cスコア.譜面情報.レベル[i];
                    for (int j = 0; j < 5; j++)
                    {
                        if (c曲リストノード.arスコア[j] != null)
                        {
                            this.n現在選択中の曲のレベル難易度毎DGB[j][i] = c曲リストノード.arスコア[j].譜面情報.レベル[i];
                            this.n現在選択中の曲のレベル小数点難易度毎DGB[j][i] = c曲リストノード.arスコア[j].譜面情報.レベルDec[i];
                            //this.n現在選択中の曲の最高ランク難易度毎[j][i] = c曲リストノード.arスコア[j].譜面情報.最大ランク[i];
                            if ( CDTXMania.ConfigIni.nSkillMode == 0 )
                            {
                                this.n現在選択中の曲の最高ランク難易度毎[ j ][ i ] = c曲リストノード.arスコア[ j ].譜面情報.最大ランク[ i ];
                            }
                            else if ( CDTXMania.ConfigIni.nSkillMode == 1 )
                            {
                                this.n現在選択中の曲の最高ランク難易度毎[ j ][ i ] = ( DTXMania.CScoreIni.tランク値を計算して返す( 0, c曲リストノード.arスコア[ j ].譜面情報.最大スキル[ i ] ) == (int)DTXMania.CScoreIni.ERANK.S && DTXMania.CScoreIni.tランク値を計算して返す( 0,  c曲リストノード.arスコア[ j ].譜面情報.最大スキル[ i ] ) >= 95 ? DTXMania.CScoreIni.tランク値を計算して返す( 0, cスコア.譜面情報.最大スキル[ i ] ) : c曲リストノード.arスコア[ j ].譜面情報.最大ランク[ i ]);
                            }
                            this.db現在選択中の曲の最高スキル値難易度毎[ j ][ i ] = c曲リストノード.arスコア[ j ].譜面情報.最大スキル[i];
                            this.b現在選択中の曲がフルコンボ難易度毎[j][i] = c曲リストノード.arスコア[j].譜面情報.フルコンボ[i];
                            this.b現在選択中の曲に譜面がある[j][i] = c曲リストノード.arスコア[j].譜面情報.b譜面がある[i];
                        }
                        else
                        {
                            this.b現在選択中の曲がフルコンボ難易度毎[j][i] = false;
                        }
                    }
				}
				for( int i = 0; i < 5; i++ )
				{
                    if (c曲リストノード.arスコア[i] != null)
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

                        this.db現在選択中の曲の曲別スキル値難易度毎[i] = c曲リストノード.arスコア[i].譜面情報.最大曲別スキル.Drums;
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

			this.n現在選択中の曲の難易度 = 0;
			for( int i = 0; i < 3; i++ )
			{
				this.n現在選択中の曲のレベル[ i ] = 0;
                this.db現在選択中の曲の曲別スキル[ i ] = 0.0;
				this.n現在選択中の曲の最高ランク[ i ] = (int)CScoreIni.ERANK.UNKNOWN;
				this.b現在選択中の曲がフルコンボ[ i ] = false;
				this.db現在選択中の曲の最高スキル値[ i ] = 0.0;
                for (int j = 0; j < 5; j++)
                {
                    this.n現在選択中の曲のレベル難易度毎DGB[j][i] = 0;
                    this.n現在選択中の曲のレベル小数点難易度毎DGB[j][i] = 0;
                    this.db現在選択中の曲の最高スキル値難易度毎[j][i] = 0.0;
                    this.n現在選択中の曲の最高ランク難易度毎[j][i] = (int)CScoreIni.ERANK.UNKNOWN;
                    this.b現在選択中の曲がフルコンボ難易度毎[j][i] = false;
                }
			}
			for( int j = 0; j < 5; j++ )
			{
				this.str難易度ラベル[ j ] = "";
                this.n選択中の曲のレベル難易度毎[ j ] = 0;

                this.db現在選択中の曲の曲別スキル値難易度毎[j] = 0.0;
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
				this.txゲージ用数字他 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_skill icon.png" ), false );
                this.tx難易度パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\5_difficulty panel.png"));
                this.tx難易度数字XG = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\6_LevelNumber.png"));
                this.txHSアイコン = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\7_panel_icons.jpg"));
                this.txRISKYアイコン = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_panel_icons2.jpg"));
                this.txBPM数字 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_BPMfont.png"));
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txパネル本体 );
				CDTXMania.tテクスチャの解放( ref this.txゲージ用数字他 );
                CDTXMania.tテクスチャの解放( ref this.tx難易度パネル );
                CDTXMania.tテクスチャの解放( ref this.tx難易度数字XG );
                CDTXMania.tテクスチャの解放( ref this.txHSアイコン );
                CDTXMania.tテクスチャの解放( ref this.txRISKYアイコン );
                CDTXMania.tテクスチャの解放( ref this.txBPM数字 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
        {

            #region [ 本体位置 ]

            for (int i = 0; i < 3; i++)
            {
                this.n本体X[i] = 0;
                this.n本体Y[i] = 0;
            }

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n本体X[0] = 346;
                this.n本体Y[0] = 43;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                {
                    this.n本体X[1] = 218;
                    this.n本体Y[1] = 546;
                    this.n本体X[2] = 346;
                    this.n本体Y[2] = 43;
                }
                else
                {
                    this.n本体X[1] = 346;
                    this.n本体Y[1] = 43;
                    this.n本体X[2] = 218;
                    this.n本体Y[2] = 546;
                }
            }
            #endregion

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
                    
                    //if( CDTXMania.stage選曲.act曲リスト.b登場アニメ全部完了 )
                    {
                            var mat1 = Matrix.Identity;
                            mat1 *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                            mat1 *= Matrix.RotationY(0.0f);
                            mat1 *= Matrix.RotationZ(0.0f);
                            mat1 *= Matrix.Translation(0f, 260f, 0f);

                            var mat2 = Matrix.Identity;
                            mat2 *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                            mat2 *= Matrix.RotationY(0.0f);
                            mat2 *= Matrix.RotationZ(0.0f);
                            mat2 *= Matrix.Translation(0f, -240f, 0f);

                        if( CDTXMania.ConfigIni.bDrums有効 )
                        {
                            if( CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[ 5 ].n現在の値 != 100 )
                                this.txパネル本体.t3D描画( CDTXMania.app.Device, mat1 , new Rectangle( 0, 0, 1280, 200 ) );
                            else
					            this.txパネル本体.t2D描画( CDTXMania.app.Device, 0, 0, new Rectangle(0, 0, 1280, 200) );
                        }
                        else if ( CDTXMania.ConfigIni.bGuitar有効 )
                        {
                            if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 != 100)
                            {
                                if (!CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                                {
                                    this.txパネル本体.t3D描画(CDTXMania.app.Device, mat1, new Rectangle(0, 200, 1280, 200));
                                    this.txパネル本体.t3D描画(CDTXMania.app.Device, mat2, new Rectangle(0, 400, 1280, 200));
                                }
                                else
                                {
                                    this.txパネル本体.t3D描画(CDTXMania.app.Device, mat1, new Rectangle(0, 600, 1280, 200));
                                    this.txパネル本体.t3D描画(CDTXMania.app.Device, mat2, new Rectangle(0, 800, 1280, 200));
                                }
                            }
                            else
                            {
                                if (!CDTXMania.ConfigIni.bIsSwappedGuitarBass)
                                {
                                    this.txパネル本体.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 200, 1280, 200));
                                    this.txパネル本体.t2D描画(CDTXMania.app.Device, 0, 500, new Rectangle(0, 400, 1280, 200));
                                }
                                else
                                {
                                    this.txパネル本体.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 600, 1280, 200));
                                    this.txパネル本体.t2D描画(CDTXMania.app.Device, 0, 500, new Rectangle(0, 800, 1280, 200));
                                }

                            }
                        }
                    }
                        /*
                    else
                    
                    {
                        var mat = Matrix.Identity;
                        mat *= Matrix.RotationX( CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[ 5 ].n現在の値 );
                        mat *= Matrix.Translation( 0f, 0f, 0f );

                        if( CDTXMania.ConfigIni.bDrums有効 && CDTXMania.ConfigIni.bGuitar有効 == false )
                            this.txパネル本体.t3D描画( CDTXMania.app.Device, mat );
					        //this.txパネル本体.t2D描画( CDTXMania.app.Device, 0, 0, new Rectangle(0, 0, 1280, 200) );
                        else if (CDTXMania.ConfigIni.bDrums有効 == false && CDTXMania.ConfigIni.bGuitar有効 == true)
                        {
                            this.txパネル本体.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 200, 1280, 200));
                            this.txパネル本体.t2D描画(CDTXMania.app.Device, 0, 500, new Rectangle(0, 400, 1280, 200));
                        }
                    }
                         */

				}
				//-----------------
				#endregion

                #region [ オプションアイコンの描画 ]
                //-----------------
                if ( this.txHSアイコン != null )
                {
                    var mat1 = Matrix.Identity;
                    mat1 *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                    mat1 *= Matrix.RotationY(0.0f);
                    mat1 *= Matrix.RotationZ(0.0f);
                    mat1 *= Matrix.Scaling(36.0f / 42.0f, 36.0f / 48.0f, 1.0f);
                    mat1 *= Matrix.Translation(519f, 374f, 0f);

                    var mat2 = Matrix.Identity;
                    mat2 *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                    mat2 *= Matrix.RotationY(0.0f);
                    mat2 *= Matrix.RotationZ(0.0f);
                    mat2 *= Matrix.Scaling(36.0f / 42.0f, 36.0f / 48.0f, 1.0f);
                    mat2 *= Matrix.Translation(300f, 200f, 0f);

                    this.txHSアイコン.vc拡大縮小倍率 = new SlimDX.Vector3(36.0f / 42.0f, 36.0f / 48.0f, 1.0f);
                    if( CDTXMania.ConfigIni.bDrums有効 )
                    {
                        if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 != 100)
                        {
//                            this.txHSアイコン.t3D描画(CDTXMania.app.Device, mat1, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Drums > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Drums) * 48), 42, 48));
                        }
                        else
                            this.txHSアイコン.t2D描画(CDTXMania.app.Device, 1067, 62, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Drums > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Drums) * 48), 42, 48));
                    }
                    else
                    {
                        if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 != 100)
                        {
//                            this.txHSアイコン.t3D描画(CDTXMania.app.Device, mat1, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Guitar > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Guitar) * 48), 42, 48));
//                            this.txHSアイコン.t3D描画(CDTXMania.app.Device, mat2, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Bass > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Bass) * 48), 42, 48));
                        }
                        else
                        {
                            this.txHSアイコン.t2D描画(CDTXMania.app.Device, 1067,  62, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Guitar > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Guitar) * 48), 42, 48));
                            this.txHSアイコン.t2D描画(CDTXMania.app.Device,  939, 565, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Bass > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Bass) * 48), 42, 48));
                        }
                    }
                }

                if (this.txRISKYアイコン != null)
                {
                    this.txRISKYアイコン.vc拡大縮小倍率 = new SlimDX.Vector3(36.0f / 42.0f, 36.0f / 48.0f, 1.0f);
                    if (CDTXMania.ConfigIni.bDrums有効)
                    {
                        if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 != 100)
                        {
                            //                            this.txHSアイコン.t3D描画(CDTXMania.app.Device, mat1, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Drums > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Drums) * 48), 42, 48));
                        }
                        else
                            this.txRISKYアイコン.t2D描画(CDTXMania.app.Device, 1151, 63, new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48));
                    }
                    else
                    {
                        if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 != 100)
                        {
                            //                            this.txHSアイコン.t3D描画(CDTXMania.app.Device, mat1, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Guitar > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Guitar) * 48), 42, 48));
                            //                            this.txHSアイコン.t3D描画(CDTXMania.app.Device, mat2, new Rectangle(0, 0 + (((CDTXMania.ConfigIni.n譜面スクロール速度.Bass > 15) ? 15 : CDTXMania.ConfigIni.n譜面スクロール速度.Bass) * 48), 42, 48));
                        }
                        else
                        {
                            this.txRISKYアイコン.t2D描画(CDTXMania.app.Device, 1151,  63, new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48));
                            this.txRISKYアイコン.t2D描画(CDTXMania.app.Device, 1024, 565, new Rectangle(0, ((CDTXMania.ConfigIni.nRisky > 10) ? 10 : CDTXMania.ConfigIni.nRisky) * 48, 42, 48));
                        }
                    }

                }


                //-----------------
                #endregion

                #region [ 難易度パネルの描画 ]
                //-----------------
                int[] y差分 = new int[5];
                int[] x差分 = new int[5];
                int flag = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (this.n現在選択中の曲の難易度 == i)
                    {
                        y差分[i] += 10;
                        x差分[i] += 132;
                    }
                }
                if ( this.tx難易度パネル != null )
                {
                    if (CDTXMania.ConfigIni.bDrums有効)
                    {
                        if( CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 == 100 )
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (this.str難易度ラベル[i] != null || CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                                    this.tx難易度パネル.t2D描画(CDTXMania.app.Device, this.n本体X[0] + (143 * i), this.n本体Y[0] - y差分[i], new Rectangle(x差分[i], 0 + (98 * i), 132, 98));
                                else if (CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE)
                                    flag = flag + 1;
                            }
                            if ( flag == 5 )
                                this.tx難易度パネル.t2D描画(CDTXMania.app.Device, this.n本体X[0] + (143 * 4), this.n本体Y[0] - 10, new Rectangle(132, 0 + (98 * 4), 132, 98));
                        }
                        else
                        {
                            for ( int i = 0; i < 5; i++ )
                            {
                                var mat = Matrix.Identity;
                                mat *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                                mat *= Matrix.RotationY(0.0f);
                                mat *= Matrix.RotationZ(0.0f);
                                mat *= Matrix.Translation(-228f + (i * 143f), 268f + y差分[i], 0f);

                                if (this.str難易度ラベル[i] != null)
                                    this.tx難易度パネル.t3D描画(CDTXMania.app.Device, mat, new Rectangle(x差分[i], 98 * i, 132, 98));
                            }
                        }
                    }
                    else if (CDTXMania.ConfigIni.bGuitar有効)
                    {
                        if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 == 100)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (this.str難易度ラベル[i] != null || CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                                    this.tx難易度パネル.t2D描画(CDTXMania.app.Device, this.n本体X[1] + (143 * i), this.n本体Y[1] - y差分[i], new Rectangle(x差分[i], 0 + (98 * i), 132, 98));
                                else if (CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE)
                                    flag = flag + 1;

                                if (this.str難易度ラベル[i] != null || CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                                    this.tx難易度パネル.t2D描画(CDTXMania.app.Device, this.n本体X[2] + (143 * i), this.n本体Y[2] - y差分[i], new Rectangle(x差分[i], 0 + (98 * i), 132, 98));
                            }
                            if (flag == 5)
                            {
                                this.tx難易度パネル.t2D描画(CDTXMania.app.Device, this.n本体X[1] + (143 * 4), this.n本体Y[1] - 10, new Rectangle(132, 0 + (98 * 4), 132, 98));
                                this.tx難易度パネル.t2D描画(CDTXMania.app.Device, this.n本体X[2] + (143 * 4), this.n本体Y[2] - 10, new Rectangle(132, 0 + (98 * 4), 132, 98));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                var mat1 = Matrix.Identity;
                                mat1 *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                                mat1 *= Matrix.RotationY(0.0f);
                                mat1 *= Matrix.RotationZ(0.0f);
                                mat1 *= Matrix.Translation(-228f + (i * 143f), 268f + y差分[i], 0f);

                                var mat2 = Matrix.Identity;
                                mat2 *= Matrix.RotationX(1.60f - (float)((float)CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 0.016f));
                                mat2 *= Matrix.RotationY(0.0f);
                                mat2 *= Matrix.RotationZ(0.0f);
                                mat2 *= Matrix.Translation(-355f + (i * 143f), -235f + y差分[i], 0f);


                                if (this.str難易度ラベル[i] != null)
                                    this.tx難易度パネル.t3D描画(CDTXMania.app.Device, mat1 , new Rectangle(0 + x差分[i], 0 + (98 * i), 132, 98));

                                if (this.str難易度ラベル[i] != null)
                                    this.tx難易度パネル.t3D描画(CDTXMania.app.Device, mat2 , new Rectangle(0 + x差分[i], 0 + (98 * i), 132, 98));
                            }
                        }
                    


                    }
                }
                //-----------------
                #endregion

                if (CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 == 100)
                {

                    #region [ 難易度文字列の描画 ]
                    //-----------------
                    for (int i = 0; i < 5; i++)
                    {
                        CDTXMania.act文字コンソール.tPrint(346 + (i * 142), 8, (this.n現在選択中の曲の難易度 == i) ? C文字コンソール.Eフォント種別.赤 : C文字コンソール.Eフォント種別.白, this.str難易度ラベル[i]);
                    }
                    //-----------------
                    #endregion

                    Cスコア cスコア = CDTXMania.stage選曲.r現在選択中のスコア;

                    #region [ 選択曲の Lv の描画 ]
                    //-----------------
                    if ((cスコア != null) && (this.tx難易度数字XG != null))
                    {
                        this.tx難易度数字XG.n透明度 = (int)(CDTXMania.stage選曲.act曲リスト.ct登場アニメ用[5].n現在の値 * 2.6f);
                        for (int j = 0; j < 3; j++)
                        {
                            if ( this.n本体X[j] != 0 )
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    int[] n難易度整数 = new int[5];
                                    int[] n難易度小数 = new int[5];
                                    if (n選択中の曲のレベル難易度毎[i] > 100)
                                    {
                                        n難易度整数[i] = (int)this.n現在選択中の曲のレベル難易度毎DGB[i][j] / 100;
                                        n難易度小数[i] = (n選択中の曲のレベル難易度毎[i] - (n難易度整数[i] * 100));
                                    }
                                    else
                                    {
                                        n難易度整数[i] = (int)this.n現在選択中の曲のレベル難易度毎DGB[i][j] / 10;
                                        n難易度小数[i] = (this.n現在選択中の曲のレベル難易度毎DGB[i][j] - (n難易度整数[i] * 10)) * 10;
                                    }

                                    if (this.str難易度ラベル[i] != null && this.b現在選択中の曲に譜面がある[i][j])
                                    {
                                        this.t大文字表示(73 + this.n本体X[j] + (i * 143), 19 + this.n本体Y[j] - y差分[i], string.Format("{0:0}", n難易度整数[i]));
                                        this.t小文字表示(102 + this.n本体X[j] + (i * 143), 37 + this.n本体Y[j] - y差分[i], string.Format("{0,2:00}", n難易度小数[i]));
                                        this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                                    }
                                    else if ((this.str難易度ラベル[i] != null && !this.b現在選択中の曲に譜面がある[i][j]) || CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.RANDOM)
                                    {
                                        this.t大文字表示(73 + this.n本体X[j] + (i * 143), 19 + this.n本体Y[j] - y差分[i], ("-"));
                                        this.t小文字表示(102 + this.n本体X[j] + (i * 143), 37 + this.n本体Y[j] - y差分[i], ("--"));
                                        this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (i * 143), 51 + this.n本体Y[j] - y差分[i], new Rectangle(145, 54, 7, 8));
                                    }
                                }
                                if (flag == 5)
                                {
                                    int n難易度整数 = 0;
                                    int n難易度小数 = 0;
                                    if (this.n現在選択中の曲のレベル[j] >= 100)
                                    {
                                        n難易度整数 = (int)this.n現在選択中の曲のレベル[j] / 100;
                                        n難易度小数 = (this.n現在選択中の曲のレベル[j] - (n難易度整数 * 100));
                                    }
                                    else
                                    {
                                        n難易度整数 = (int)this.n現在選択中の曲のレベル[j] / 10;
                                        n難易度小数 = (this.n現在選択中の曲のレベル[j] - (n難易度整数 * 10)) * 10;
                                    }

                                    if (this.b現在選択中の曲の譜面[j] && CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE)
                                    {
                                        this.t大文字表示(73 + this.n本体X[j] + (4 * 143), 19 + this.n本体Y[j] - 10, string.Format("{0:0}", n難易度整数));
                                        this.t小文字表示(102 + this.n本体X[j] + (4 * 143), 37 + this.n本体Y[j] - 10, string.Format("{0,2:00}", n難易度小数));
                                        this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (4 * 143), 51 + this.n本体Y[j] - 10, new Rectangle(145, 54, 7, 8));
                                    }
                                    else if (!this.b現在選択中の曲の譜面[j] && CDTXMania.stage選曲.r現在選択中の曲.eノード種別 == C曲リストノード.Eノード種別.SCORE)
                                    {
                                        this.t大文字表示(73 + this.n本体X[j] + (4 * 143), 19 + this.n本体Y[j] - 10, ("-"));
                                        this.t小文字表示(102 + this.n本体X[j] + (4 * 143), 37 + this.n本体Y[j] - 10, ("--"));
                                        this.tx難易度数字XG.t2D描画(CDTXMania.app.Device, 94 + this.n本体X[j] + (4 * 143), 51 + this.n本体Y[j] - 10, new Rectangle(145, 54, 7, 8));
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region [ 選択曲の BPM の描画 ]
                    if (CDTXMania.stage選曲.r現在選択中の曲 != null)
                    {
                        string strBPM;
                        switch (CDTXMania.stage選曲.r現在選択中の曲.eノード種別)
                        {
                            case C曲リストノード.Eノード種別.SCORE:
                                {
                                    strBPM = cスコア.譜面情報.Bpm.ToString();
                                    break;
                                }
                            default:
                                {
                                    strBPM = "---";
                                    break;
                                }
                        }

                        this.tBPM表示(502, 405, string.Format("{0,3:###}", strBPM));
                        //CDTXMania.act文字コンソール.tPrint(50, 570, C文字コンソール.Eフォント種別.白, string.Format("BPM:{0:####0}", this.n現在選択中の曲のBPM));
                    }
                    //-----------------
                    #endregion
                    #region [ 選択曲の 曲別スキルの描画 ]
                    //-----------------
                    for (int i = 0; i < 5; i++)
                    {
                        if (this.str難易度ラベル[i] != null)
                        {
                            double[] db現在の曲のレベルXG = new double[5];
                            if (this.n選択中の曲のレベル難易度毎[i] < 100)
                            {
                                db現在の曲のレベルXG[i] = this.n選択中の曲のレベル難易度毎[i] / 10.0;
                            }
                            else
                            {
                                db現在の曲のレベルXG[i] = this.n選択中の曲のレベル難易度毎[i] / 100.0;
                            }

                            double db1 = Math.Max(this.db現在選択中の曲の最高スキル値難易度毎[0].Drums * db現在の曲のレベルXG[0] * 20, this.db現在選択中の曲の曲別スキル値難易度毎[1] * db現在の曲のレベルXG[1] * 20);
                            double db2 = Math.Max(this.db現在選択中の曲の曲別スキル値難易度毎[2] * db現在の曲のレベルXG[2] * 20, this.db現在選択中の曲の曲別スキル値難易度毎[3] * db現在の曲のレベルXG[3] * 20);
                            double dbA = Math.Max(db1, db2);
                            double db曲別スキル = Math.Max(dbA, this.db現在選択中の曲の曲別スキル値難易度毎[4] * db現在の曲のレベルXG[4] * 20);

                            //this.t達成率表示(250, 120, string.Format("{0,6:##0.00}", db曲別スキル));
                        }
                    }
                    //-----------------
                    #endregion
                    #region [ 選択曲の 最高スキル値ゲージ＋数値の描画 ]
                    //-----------------
                    for (int j = 0; j < 3; j++)
                    {
                        if ( this.n本体X[j] != 0 )
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (this.str難易度ラベル[i] != null && this.db現在選択中の曲の最高スキル値難易度毎[i][j] != 0.00)
                                {
                                    this.t達成率表示(83 + this.n本体X[j] + (i * 143), 77 + this.n本体Y[j] - y差分[i], string.Format("{0,6:##0.00}%", this.db現在選択中の曲の最高スキル値難易度毎[i][j]));
                                }
                            }
                            if (flag == 5 && this.db現在選択中の曲の最高スキル値[j] != 0.00)
                            {
                                this.t達成率表示(83 + this.n本体X[j] + (4 * 143), 77 + this.n本体Y[j] - 10, string.Format("{0,6:##0.00}%", this.db現在選択中の曲の最高スキル値[j]));
                            }
                        }
                    }
                    //-----------------
                    #endregion
                    #region [ 選択曲の 最高ランクの描画 ]
                    //-----------------
                    for (int j = 0; j < 3; j++)
                    {
                        if (this.n本体X[j] != 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                int nMaxRank = this.n現在選択中の曲の最高ランク難易度毎[i][j];
                                if (nMaxRank != 99)
                                {
                                    if (nMaxRank < 0)
                                    {
                                        nMaxRank = 0;
                                    }
                                    if (nMaxRank > 6)
                                    {
                                        nMaxRank = 6;
                                    }
                                    if (this.txゲージ用数字他 != null && this.str難易度ラベル[i] != null)
                                    {
                                        this.txゲージ用数字他.t2D描画(CDTXMania.app.Device, (7 + this.n本体X[j] + (i * 143)), 61 + this.n本体Y[j] - y差分[i], new Rectangle(42 + (nMaxRank * 32), 0, 32, 32));
                                    }
                                }
                            }
                        }
                    }
                    //-----------------
                    #endregion
                    #region [ 選択曲の FullCombo Excellent の 描画 ]
                    //-----------------
                    for (int j = 0; j < 3; j++)
                    {
                        if (this.n本体X[j] != 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (this.db現在選択中の曲の最高スキル値難易度毎[i][j] == 100 && this.str難易度ラベル[i] != null)
                                {
                                    if (this.txゲージ用数字他 != null)
                                    {
                                        this.txゲージ用数字他.t2D描画(CDTXMania.app.Device, 40 + this.n本体X[j] + (i * 143), 63 + this.n本体Y[j] - y差分[i], new Rectangle(266, 0, 42, 32));
                                    }
                                }
                                else if (this.b現在選択中の曲がフルコンボ難易度毎[i][j] && this.str難易度ラベル[i] != null)
                                {
                                    if (this.txゲージ用数字他 != null)
                                    {
                                        this.txゲージ用数字他.t2D描画(CDTXMania.app.Device, 40 + this.n本体X[j] + (i * 143), 63 + this.n本体Y[j] - y差分[i], new Rectangle(0, 0, 42, 32));
                                    }
                                }
                            }
                            if (flag == 5)
                            {
                                if (this.db現在選択中の曲の最高スキル値[j] == 100)
                                {
                                    if (this.txゲージ用数字他 != null)
                                    {
                                        this.txゲージ用数字他.t2D描画(CDTXMania.app.Device, 40 + this.n本体X[j] + (4 * 143), 63 + this.n本体Y[j] - 10, new Rectangle(266, 0, 42, 32));
                                    }
                                }
                                else if (this.b現在選択中の曲がフルコンボ[j])
                                {
                                    if (this.txゲージ用数字他 != null)
                                    {
                                        this.txゲージ用数字他.t2D描画(CDTXMania.app.Device, 40 + this.n本体X[j] + (4 * 143), 63 + this.n本体Y[j] - 10, new Rectangle(0, 0, 42, 32));
                                    }
                                }
                            }
                        }
                    }
                    //-----------------
                    #endregion

                }
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

        private STDGBVALUE<int> n本体X;
        private STDGBVALUE<int> n本体Y;

		private STDGBVALUE<bool> b現在選択中の曲がフルコンボ;
        private STDGBVALUE<bool> b現在選択中の曲の譜面;
        private STDGBVALUE<bool>[] b現在選択中の曲がフルコンボ難易度毎 = new STDGBVALUE<bool>[5];
        private STDGBVALUE<bool>[] b現在選択中の曲に譜面がある = new STDGBVALUE<bool>[5];
        private STDGBVALUE<int>[] n現在選択中の曲のレベル難易度毎DGB = new STDGBVALUE<int>[5];
        private STDGBVALUE<int>[] n現在選択中の曲のレベル小数点難易度毎DGB = new STDGBVALUE<int>[5];
		private CCounter ct登場アニメ用;
		private CCounter ct難易度スクロール用;
		private CCounter ct難易度矢印用;
		private STDGBVALUE<double> db現在選択中の曲の最高スキル値;
        private STDGBVALUE<double>[] db現在選択中の曲の最高スキル値難易度毎 = new STDGBVALUE<double>[5];
        private double[] db現在選択中の曲の曲別スキル値難易度毎 = new double[5];
        private STDGBVALUE<double> db現在選択中の曲の曲別スキル;
		private STDGBVALUE<int> n現在選択中の曲のレベル;
        private int[] n選択中の曲のレベル難易度毎 = new int[5];
		private STDGBVALUE<int> n現在選択中の曲の最高ランク;
        private STDGBVALUE<int>[] n現在選択中の曲の最高ランク難易度毎 = new STDGBVALUE<int>[5];
		private int n現在選択中の曲の難易度;
		private int n難易度開始文字位置;
		private const int n難易度表示可能文字数 = 0x24;
        private readonly Rectangle[] rcランク = new Rectangle[] { new Rectangle(0, 0x30, 20, 15), new Rectangle(20, 0x30, 20, 15), new Rectangle(40, 0x30, 20, 15), new Rectangle(0, 0x3f, 20, 15), new Rectangle(20, 0x3f, 20, 15), new Rectangle(40, 0x3f, 20, 15), new Rectangle(0, 0x4e, 20, 15) };
        private readonly Rectangle[] rc数字 = new Rectangle[] { new Rectangle(0, 0, 15, 0x13), new Rectangle(15, 0, 15, 0x13), new Rectangle(30, 0, 15, 0x13), new Rectangle(0x2d, 0, 15, 0x13), new Rectangle(0, 0x13, 15, 0x13), new Rectangle(15, 0x13, 15, 0x13), new Rectangle(30, 0x13, 15, 0x13), new Rectangle(0x2d, 0x13, 15, 0x13), new Rectangle(0, 0x26, 15, 0x13), new Rectangle(15, 0x26, 15, 0x13), new Rectangle(30, 0x26, 15, 0x13), new Rectangle(0x2d, 0x26, 15, 0x13) };
		private C曲リストノード r直前の曲;
		public string[] str難易度ラベル = new string[] { "", "", "", "", "" };
        private readonly ST数字[] st数字 = new ST数字[] 
        { new ST数字('0', new Rectangle(0, 0, 6, 10)),
          new ST数字('1', new Rectangle(6, 0, 6, 10)),
          new ST数字('2', new Rectangle(12, 0, 6, 10)),
          new ST数字('3', new Rectangle(18, 0, 6, 10)),
          new ST数字('4', new Rectangle(24, 0, 6, 10)),
          new ST数字('5', new Rectangle(30, 0, 6, 10)),
          new ST数字('6', new Rectangle(36, 0, 6, 10)),
          new ST数字('7', new Rectangle(42, 0, 6, 10)),
          new ST数字('8', new Rectangle(48, 0, 6, 10)),
          new ST数字('9', new Rectangle(54, 0, 6, 10)),
          new ST数字('-', new Rectangle(60, 0, 6, 10)),
          new ST数字('p', new Rectangle(0, 0, 0, 0)) };
        private readonly ST達成率数字[] st達成率数字 = new ST達成率数字[] { new ST達成率数字('0', new Rectangle(0, 62, 7, 16)), new ST達成率数字('1', new Rectangle(7, 62, 7, 16)), new ST達成率数字('2', new Rectangle(14, 62, 7, 16)), new ST達成率数字('3', new Rectangle(21, 62, 7, 16)), new ST達成率数字('4', new Rectangle(28, 62, 7, 16)), new ST達成率数字('5', new Rectangle(35, 62, 7, 16)), new ST達成率数字('6', new Rectangle(42, 62, 7, 16)), new ST達成率数字('7', new Rectangle(49, 62, 7, 16)), new ST達成率数字('8', new Rectangle(56, 62, 7, 16)), new ST達成率数字('9', new Rectangle(63, 62, 7, 16)), new ST達成率数字('%', new Rectangle(70, 62, 9, 16)), new ST達成率数字('.', new Rectangle(79, 62, 3, 16)) };
        private readonly Rectangle rcunused = new Rectangle(0, 0x21, 80, 15);
		private CTexture txゲージ用数字他;
		private CTexture txパネル本体;
        private CTexture tx難易度パネル;
        private CTexture tx難易度数字XG;
        private CTexture txHSアイコン;
        private CTexture txRISKYアイコン;
        private CTexture txBPM数字;
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
        private void tBPM表示(int x, int y, string str)
        {
            for (int j = 0; j < str.Length; j++)
            {
                for (int i = 0; i < this.st数字.Length; i++)
                {
                    if (this.st数字[i].ch == str[j])
                    {
                        Rectangle rectangle = new Rectangle(this.st数字[i].rc.X, this.st数字[i].rc.Y, 6, 10);
                        if (this.txBPM数字 != null)
                        {
                            this.txBPM数字.t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += 7;
            }
        }
		//-----------------
		#endregion
	}
}
