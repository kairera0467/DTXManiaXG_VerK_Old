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
            if( !base.b活性化してない )
            {
                if( base.b初めての進行描画 )
                {
                    base.b初めての進行描画 = false;
                }

                //ゲージ部分
                if( this.txLivePointゲージ != null )
                {
                    this.txLivePointゲージ.t2D描画( CDTXMania.app.Device, 886, 52, new Rectangle( 0, 0, 71, 668 ) );
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
                        this.txLivePoint.t2D描画(CDTXMania.app.Device, 895 + (i * 18), 685, rectangle);
                    }
                }
                #region[箱]
                //まず箱を再現するためにはLPが一定以上になったら表示させるような仕掛けが必要。
                if( base.n現在のLivePoint.Drums >= 0 )
                {
                    if( this.n現在のLivePoint.Drums >= 20 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 626, new Rectangle( 71, 536, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 40 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 585, new Rectangle( 71, 536, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 60 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 544, new Rectangle( 71, 536, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 80 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 503, new Rectangle( 71, 536, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 100 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 462, new Rectangle( 71, 536, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 120 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 420, new Rectangle( 71, 580, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 140 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 379, new Rectangle( 71, 580, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 160 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 338, new Rectangle( 71, 580, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 180 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 297, new Rectangle( 71, 580, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 200 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 256, new Rectangle( 71, 580, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 220 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 214, new Rectangle( 71, 624, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 240 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 173, new Rectangle( 71, 624, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 260 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 132, new Rectangle( 71, 624, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 280 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 91, new Rectangle( 71, 624, 46, 44 ) );
                    }
                    if( this.n現在のLivePoint.Drums >= 300 )
                    {
                        this.txLivePoint.t2D描画( CDTXMania.app.Device, 887, 50, new Rectangle( 71, 624, 46, 44 ) );
                    }
                }
                #endregion
            }
            return 0;
        }
    }
}
