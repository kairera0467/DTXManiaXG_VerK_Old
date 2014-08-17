using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsドラムセット : CActivity
    {
        /// <summary>
        /// ドラムを描画するクラス。
        /// 
        /// 課題
        /// ・オート時の対応
        /// ・シンバルの実装
        /// ・加速度の調整
        /// </summary>
        public CAct演奏Drumsドラムセット()
        {
            base.b活性化してない = true;
        }

        public void Start( int nlane )
        {
            this.stパッド状態[ nlane ].nY座標加速度dot = 2;
        }

        public override void On活性化()
        {
            this.nY座標制御タイマ = -1;
            for( int i = 0; i < 9; i++ )
            {
                STパッド状態 stパッド状態 = new STパッド状態();
                stパッド状態.nY座標オフセットdot = 0;
                stパッド状態.nY座標加速度dot = 0;
                this.stパッド状態[ i ] = stパッド状態;
            }

            this.nLeftCymbalFrame = 9;
            this.nLeftCymbalInterval = 35;

            this.ctLeftCymbal = new CCounter( 1, this.nLeftCymbalFrame, this.nLeftCymbalInterval, CDTXMania.Timer );
            this.ctLeftCymbal = new CCounter( 1, this.nRightCymbalFrame, this.nRightCymbalInterval, CDTXMania.Timer );

            base.On活性化();
        }

        public override void On非活性化()
        {
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            this.txSnare = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_Snare.png" ) );
            this.txHitom = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_HiTom.png" ) );
            this.txLowTom = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_LowTom.png" ) );
            this.txFloorTom = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_FloorTom.png" ) );
            this.txBassDrum = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_BassDrum.png" ) );


            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txSnare );
            CDTXMania.tテクスチャの解放( ref this.txHitom );
            CDTXMania.tテクスチャの解放( ref this.txLowTom );
            CDTXMania.tテクスチャの解放( ref this.txFloorTom );
            CDTXMania.tテクスチャの解放( ref this.txBassDrum );

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( base.b初めての進行描画 )
			{
				this.nY座標制御タイマ = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
				base.b初めての進行描画 = false;
			}
			long num = FDK.CSound管理.rc演奏用タイマ.n現在時刻;
			if( num < this.nY座標制御タイマ )
			{
				this.nY座標制御タイマ = num;
			}
			while( ( num - this.nY座標制御タイマ ) >= 5 )
			{
				for( int k = 0; k < 10; k++ )
				{
					this.stパッド状態[ k ].nY座標オフセットdot += this.stパッド状態[ k ].nY座標加速度dot;
					if( this.stパッド状態[ k ].nY座標オフセットdot > 15 )
					{
						this.stパッド状態[ k ].nY座標オフセットdot = 15;
						this.stパッド状態[ k ].nY座標加速度dot = -1;
					}
					else if( this.stパッド状態[ k ].nY座標オフセットdot < 0 )
					{
						this.stパッド状態[ k ].nY座標オフセットdot = 0;
						this.stパッド状態[ k ].nY座標加速度dot = 0;
					}
				}
			    this.nY座標制御タイマ += 5;
            }
            #region[ 座標類 ]
            this.n座標Snare = 490 + this.stパッド状態[ 2 ].nY座標オフセットdot;
            this.n座標HiTom = 491 + this.stパッド状態[ 4 ].nY座標オフセットdot;
            this.n座標LowTom = 490 + this.stパッド状態[ 5 ].nY座標オフセットdot;
            this.n座標FloorTom = 490 + this.stパッド状態[ 6 ].nY座標オフセットdot;
            this.n座標BassDrum = 517 - this.stパッド状態[ 3 ].nY座標オフセットdot;
            #endregion

            #region[ スネア ]
            this.txSnare.t2D描画( CDTXMania.app.Device, 0, this.n座標Snare );
            #endregion

            #region[ ハイタム ]
            this.txHitom.t2D描画( CDTXMania.app.Device, 107, this.n座標HiTom );
            #endregion

            #region[ フロアタム ]
            this.txFloorTom.t2D描画( CDTXMania.app.Device, 1049, this.n座標FloorTom );
            #endregion

            #region[ ロータム ]
            this.txLowTom.t2D描画( CDTXMania.app.Device, 870, this.n座標LowTom );
            #endregion

            #region[ バスドラ ]
            this.txBassDrum.t2D描画( CDTXMania.app.Device, 310, this.n座標BassDrum );
            #endregion

            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct STパッド状態
        {
            public int n明るさ;
            public int nY座標オフセットdot;
            public int nY座標加速度dot;
        }

        private CCounter ctLeftCymbal;
        private CCounter ctRightCymbal;
        
        private CTexture txLeftCymbal;
        private CTexture txSnare;
        private CTexture txHitom;
        private CTexture txBassDrum;
        private CTexture txLowTom;
        private CTexture txFloorTom;
        private CTexture txRightCymbal;

        private long nY座標制御タイマ;
        private STパッド状態[] stパッド状態 = new STパッド状態[10];

        private int n座標Snare;
        private int n座標HiTom;
        private int n座標LowTom;
        private int n座標FloorTom;
        private int n座標BassDrum;

        private int nLeftCymbalX;
        private int nLeftCymbalY;
        private int nLeftCymbalFrame;
        private int nLeftCymbalInterval;

        private int nRightCymbalX;
        private int nRightCymbalY;
        private int nRightCymbalFrame;
        private int nRightCymbalInterval;
        //-----------------
        #endregion
    }
}
