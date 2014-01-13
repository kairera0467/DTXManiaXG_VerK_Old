using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsLivePoint : CAct演奏LivePoint共通
    {
        // CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {

            int n本体X = 886;
            int n本体Y = 52;

            if( !base.b活性化してない )
            {
                if( base.b初めての進行描画 )
                {
                    base.b初めての進行描画 = false;
                }

                //ゲージ部分
                if( this.txLivePointゲージ != null )
                {
                    this.txLivePointゲージ.t2D描画( CDTXMania.app.Device, n本体X, n本体Y, new Rectangle( 0, 0, 71, 668 ) );
                }

                string str = string.Format( "{0,3:##0}", base.n現在のLivePoint.Drums );
                for( int i = 0; i < 3; i++ )
                {
                    Rectangle rectangle;
                    char ch = str[i];
                    if (ch.Equals(' '))
                    {
                        rectangle = new Rectangle( 72, 250, 17, 25 );
                    }
                    else
                    {
                        int num3 = int.Parse( str.Substring( i, 1 ) );
                        if ( num3 < 5 )
                        {
                            rectangle = new Rectangle( 72, ( num3 * 25 ), 17, 25 );
                        }
                        else
                        {
                            rectangle = new Rectangle( 72, ( num3 * 25 ), 17, 25 );
                        }
                    }
                    if( base.txLivePoint != null )
                    {
                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 9 + n本体X + (i * 18), 633 + n本体Y, rectangle);
                    }
                }
                #region[箱]
                //まず箱を再現するためにはLPが一定以上になったら表示させるような仕掛けが必要。
                if( base.n現在のLivePoint.Drums >= 0 )
                {
                    for (int i = 0; i < 5; i++ )
                    {
                        if (this.n現在のLivePoint.Drums >= 20 + ( 20 * i ) )
                        {
                            this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体X + 1, 574 + n本体Y - (41 * i), new Rectangle(71, 536, 46, 44));
                        }
                        if (this.n現在のLivePoint.Drums >= 120 + (20 * i))
                        {
                            this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体X + 1, 368 + n本体Y - (41 * i), new Rectangle(71, 580, 46, 44));
                        }
                        if (this.n現在のLivePoint.Drums >= 220 + (20 * i))
                        {
                            this.txLivePoint.t2D描画(CDTXMania.app.Device, n本体X + 1, 162 + n本体Y - (41 * i), new Rectangle(71, 624, 46, 44));
                        }
                    }
                }
                #endregion
            }
            return 0;
        }
    }
}
