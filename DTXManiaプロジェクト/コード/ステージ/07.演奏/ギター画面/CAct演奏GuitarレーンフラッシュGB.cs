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
            base.OnManagedリソースの作成();
        }
        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txレーン );
            base.OnManagedリソースの解放();
        }

		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{

                this.txレーン.t2D描画(CDTXMania.app.Device, 86, 104, new Rectangle(0, 0, 246, 566));
                this.txレーン.t2D描画(CDTXMania.app.Device, 67, 670, new Rectangle(0, 567, 265, 50));
                this.txレーン.t2D描画(CDTXMania.app.Device, 288, 42, new Rectangle(0, 618, 48, 62));

				for( int i = 0; i < 10; i++ )
				{
					if( !base.ct進行[ i ].b停止中 )
					{
						E楽器パート e楽器パート = ( i < 5 ) ? E楽器パート.GUITAR : E楽器パート.BASS;
						CTexture texture = CDTXMania.ConfigIni.bReverse[ (int) e楽器パート ] ? base.txFlush[ ( i % 5 ) + 5 ] : base.txFlush[ i % 5 ];
						int num2 = CDTXMania.ConfigIni.bLeft[ (int) e楽器パート ] ? 1 : 0;
						for( int j = 0; j < 5; j++ )
						{
							//int x = ( ( ( i < 5 ) ? 88 : 480 ) + this.nRGBのX座標[ num2, i ] ) + ( ( 37 * base.ct進行[ i ].n現在の値 ) / 100 );
                            int x = (((i < 5) ? 88 : 959) + this.nRGBのX座標[num2, i]);
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
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
		private readonly int[,] nRGBのX座標 = new int[ , ] { { 0, 39, 78, 117, 156, 0, 39, 78, 117, 156 }, { 156, 117, 78, 39, 0, 156, 117, 78, 39, 0 } };

        private CTexture txレーン;
		//-----------------
		#endregion
	}
}
