using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Drumsドラムセット : CActivity
	{
		// コンストラクタ

		public CAct演奏Drumsドラムセット()
		{

			base.b活性化してない = true;
		}
		
		
		// メソッド
        public override void On非活性化()
        {
            this.ct左シンバル = null;
            this.ct右シンバル = null;

            base.On非活性化();
        }
		// CActivity 実装

		public override void On活性化()
		{
            this.ct右シンバル = new CCounter(0, 8, 35, CDTXMania.Timer);
            this.ct左シンバル = new CCounter(0, 8, 35, CDTXMania.Timer);
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
            if (!base.b活性化してない)
            {
                this.tx左シンバル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_LCymbal.png" ) );
                this.tx右シンバル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_RCymbal.png" ) );
                this.txスネア = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_Snare.png" ) );
                this.txハイタム = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_HiTom.png" ) );
                this.txロータム = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_LowTom.png" ) );
                this.txフロアタム = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_FloorTom.png" ) );
                this.txバスドラ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Drums_BassDrum.png" ) );

                base.OnManagedリソースの作成();
            }
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.tx左シンバル );
                CDTXMania.tテクスチャの解放( ref this.tx右シンバル );
                CDTXMania.tテクスチャの解放( ref this.txスネア );
                CDTXMania.tテクスチャの解放( ref this.txハイタム );
                CDTXMania.tテクスチャの解放( ref this.txロータム );
                CDTXMania.tテクスチャの解放( ref this.txフロアタム );
                CDTXMania.tテクスチャの解放( ref this.txバスドラ );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
            {
                int RCym = this.ct右シンバル.n現在の値;
                int LCym = this.ct左シンバル.n現在の値;

                #region[動くドラムセット]
                for( int i = 0; i < 10; i++ )
                {
                    int index = this.n描画順[ i ];
                    this.nスネアY = 490 + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[ 2 ].nY座標オフセットdot;
                    this.nハイタムY = 491 + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[ 4 ].nY座標オフセットdot;
                    this.nバスドラムY = 517 - CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[ 3 ].nY座標オフセットdot;
                    this.nロータムY = 490 + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[ 5 ].nY座標オフセットdot;
                    this.nフロアタムY = 490 + CDTXMania.stage演奏ドラム画面.actPad.stパッド状態[ 6 ].nY座標オフセットdot;
                    if( CDTXMania.ConfigIni.eドラムセットを動かす == Eタイプ.B )
                    {
                        this.nスネアY = 490;
                        this.nハイタムY = 491;
                        this.nバスドラムY = 517;
                        this.nロータムY = 490;
                        this.nフロアタムY = 490;
                    }

                    if( index == 0 )
                    {
                        this.ct左シンバル.t進行();
                        if( this.tx左シンバル != null )
                        {
                            this.tx左シンバル.t2D描画( CDTXMania.app.Device, -120 + ( CDTXMania.stage演奏ドラム画面.ct登場用.n現在の値 * 10 ), 0, new Rectangle( 0 + ( 380 * LCym ), 0, 380, 720 ) );
                        }
                    }
                    if( index == 2 )
                    {
                        if( this.txスネア != null )
                        {
                            this.txスネア.t2D描画( CDTXMania.app.Device, 0, this.nスネアY );
                        }
                    }
                    if( index == 4 )
                    {
                        if( this.txハイタム != null )
                        {
                            this.txハイタム.t2D描画( CDTXMania.app.Device, 107, this.nハイタムY );
                        }
                    }
                    if( index == 3 )
                    {
                        if( this.txバスドラ != null )
                        {
                            this.txバスドラ.t2D描画( CDTXMania.app.Device, 310, this.nバスドラムY );
                        }
                    }
                    if( index == 5 )
                    {
                        if( this.txロータム != null )
                        {
                            this.txロータム.t2D描画( CDTXMania.app.Device, 870, this.nロータムY );
                        }
                    }
                    if( index == 6 )
                    {
                        if( this.txフロアタム != null )
                        {
                            this.txフロアタム.t2D描画( CDTXMania.app.Device, 1049, this.nフロアタムY );
                        }
                    }
                    if( index == 7 )
                    {
                        this.ct右シンバル.t進行();
                        if( this.tx右シンバル != null )
                        {
                            this.tx右シンバル.t2D描画( CDTXMania.app.Device, 1020 - ( CDTXMania.stage演奏ドラム画面.ct登場用.n現在の値 * 10 ), 0, new Rectangle( 0 + ( 380 * RCym ), 0, 380, 720 ) );
                        }
                    }

                }
                #endregion
            }
			return 0;
		}




		// その他

		#region [ private ]
		//-----------------


        public CCounter ct右シンバル;
        public CCounter ct左シンバル;

        private CTexture tx左シンバル;
        private CTexture txスネア;
        private CTexture txハイタム;
        private CTexture txバスドラ;
        private CTexture txロータム;
        private CTexture txフロアタム;
        private CTexture tx右シンバル;

        private int nスネアY;
        private int nハイタムY;
        private int nバスドラムY;
        private int nロータムY;
        private int nフロアタムY;

        private readonly int[] n描画順 = new int[] { 9, 2, 4, 6, 5, 3, 1, 8, 7, 0 };
		//-----------------
		#endregion
	}
}
