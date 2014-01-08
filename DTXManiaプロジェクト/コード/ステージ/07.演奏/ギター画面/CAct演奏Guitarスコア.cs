using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarスコア : CAct演奏スコア共通
	{
		// コンストラクタ

		public CAct演奏Guitarスコア()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装（共通クラスからの差分のみ）

		public override unsafe int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( base.b初めての進行描画 )
				{
					base.n進行用タイマ = CDTXMania.Timer.n現在時刻;
					base.b初めての進行描画 = false;
				}
				long num = CDTXMania.Timer.n現在時刻;
				if( num < base.n進行用タイマ )
				{
					base.n進行用タイマ = num;
				}
				while( ( num - base.n進行用タイマ ) >= 10 )
				{
					for( int j = 0; j < 3; j++ )
					{
						this.n現在表示中のスコア[ j ] += this.nスコアの増分[ j ];

						if( this.n現在表示中のスコア[ j ] > (long) this.n現在の本当のスコア[ j ] )
							this.n現在表示中のスコア[ j ] = (long) this.n現在の本当のスコア[ j ];
					}
					base.n進行用タイマ += 10;
				}
				for( int i = 1; i < 3; i++ )
				{
					string str = this.n現在表示中のスコア[ i ].ToString( "0000000" );
					for( int k = 0; k < 7; k++ )
					{
						Rectangle rectangle;
						char ch = str[ k ];
						if( ch.Equals( ' ' ) )
						{
							rectangle = new Rectangle( 0, 0, 32, 36 );
						}
						else
						{
							int num5 = int.Parse( str.Substring( k, 1 ) );
							if( num5 < 5 )
							{
								rectangle = new Rectangle( num5 * 32, 0, 32, 36 );
							}
							else
							{
								rectangle = new Rectangle( ( num5 * 32), 0, 32, 36 );
							}
						}
						if( base.txScore != null )
						{
							base.txScore.t2D描画( CDTXMania.app.Device, this.ptSCORE[ i - 1 ].X + ( k * 26 ), this.ptSCORE[ i - 1 ].Y, rectangle );
                            base.txScore.vc拡大縮小倍率.X = 0.85f;
                            base.txScore.vc拡大縮小倍率.Y = 0.85f;
						}
					}
				}
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		private readonly Point[] ptSCORE = new Point[] { new Point( 398, 394 ), new Point( 0, 5000 ) };
		//-----------------
		#endregion
	}
}
