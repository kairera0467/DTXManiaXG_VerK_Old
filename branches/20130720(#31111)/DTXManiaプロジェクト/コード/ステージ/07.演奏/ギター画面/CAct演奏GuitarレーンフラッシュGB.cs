using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏GuitarレーンフラッシュGB : CAct演奏レーンフラッシュGB共通
	{
		// コンストラクタ

		public CAct演奏GuitarレーンフラッシュGB()
		{
			base.b活性化してない = true;
		}
        // 2013.02.22 kairera0467
        // ギターのレーンフラッシュの幅は37。

		// CActivity 実装（共通クラスからの差分のみ）

        public override void OnManagedリソースの作成()
        {
            this.txレーン = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Paret_Guitar.png"));
            this.txレーンフラッシュ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_guitar line.png"));

            base.OnManagedリソースの作成();
        }
        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txレーン );
            CDTXMania.tテクスチャの解放( ref this.txレーンフラッシュ );
            base.OnManagedリソースの解放();
        }

		public override int On進行描画()
		{
			if( !base.b活性化してない )
            {
                #region[ レーンの描画 ]
                //---------------
                //レ－ンのみ先に描画しておく。
                if (CDTXMania.DTX.bチップがある.Guitar)
                {
                    this.txレーン.t2D描画(CDTXMania.app.Device, 86, 104, new Rectangle(0, 0, 246, 566));
                    this.txレーン.t2D描画(CDTXMania.app.Device, 67, 670, new Rectangle(0, 567, 265, 50));
                    this.txレーン.t2D描画(CDTXMania.app.Device, 288, 42, new Rectangle(0, 618, 48, 62));
                }
                if (CDTXMania.DTX.bチップがある.Bass)
                {
                    this.txレーン.t2D描画(CDTXMania.app.Device, 956, 104, new Rectangle(0, 0, 246, 566));
                    //this.txレーン.t2D描画(CDTXMania.app.Device, 67, 670, new Rectangle(0, 567, 265, 50));
                    //this.txレーン.t2D描画(CDTXMania.app.Device, 288, 42, new Rectangle(0, 618, 48, 62));
                }
                //---------------
                #endregion

                for ( int i = 0; i < 10; i++ )
				{
					if( !base.ct進行[ i ].b停止中 )
					{
						E楽器パート e楽器パート = ( i < 5 ) ? E楽器パート.GUITAR : E楽器パート.BASS;
						CTexture texture = CDTXMania.ConfigIni.bReverse[ (int) e楽器パート ] ? base.txFlush[ ( i % 5 ) + 5 ] : base.txFlush[ i % 5 ];
						int num2 = CDTXMania.ConfigIni.bLeft[ (int) e楽器パート ] ? 1 : 0;
						for( int j = 0; j < 5; j++ )
						{
							//int x = ( ( ( i < 5 ) ? 88 : 480 ) + this.nRGBのX座標[ num2, i ] ) + ( ( 37 * base.ct進行[ i ].n現在の値 ) / 100 );
                            int x = (((i < 5) ? 88 : 958) + this.nRGBのX座標[num2, i]);
							int y = CDTXMania.ConfigIni.bReverse[ (int) e楽器パート ] ? ( 55 + ( j * 118 ) ) : 100 + ( j * 118 );
							if( texture != null )
							{
                                texture.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(j * 37, 0, 37, 118));
							}
						}
						base.ct進行[ i ].t進行();
						if( base.ct進行[ i ].b終了値に達した )
						{
							base.ct進行[ i ].t停止();
						}
					}
				}
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[0] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 86, 105, new Rectangle(0, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[ 1 ] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 125, 105, new Rectangle(39, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[2] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 164, 105, new Rectangle(78, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[3] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 203, 105, new Rectangle(117, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[4] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 242, 105, new Rectangle(156, 0, 41, 566));
                }

                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[5] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 956, 105, new Rectangle(0, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[6] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 997, 105, new Rectangle(39, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[7] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 1034, 105, new Rectangle(78, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[8] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 1073, 105, new Rectangle(117, 0, 41, 566));
                }
                if (CDTXMania.stage演奏ギター画面.actRGB.b押下状態[9] == true)
                {
                    this.txレーンフラッシュ.t2D描画(CDTXMania.app.Device, 1112, 105, new Rectangle(156, 0, 41, 566));
                }
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
		private readonly int[,] nRGBのX座標 = new int[ , ] { { 0, 39, 78, 117, 156, 0, 39, 78, 117, 156 }, { 156, 117, 78, 39, 0, 156, 117, 78, 39, 0 } };

        private CTexture txレーン;
        private CTexture txレーンフラッシュ;
		//-----------------
		#endregion
	}
}
