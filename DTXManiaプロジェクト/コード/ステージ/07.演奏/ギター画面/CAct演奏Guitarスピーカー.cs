using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Guitarスピーカー : CActivity
    {
        protected CTexture txスピーカー;
        public CCounter ctBPM;
        public double UnitTime;

        public override void On活性化()
        {
            this.ctBPM = null;
            this.UnitTime = 0.0;
            base.On活性化();
        }
        public override void On非活性化()
        {
            this.ctBPM = null;
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if( !base.b活性化してない )
            {
                this.txスピーカー = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_speaker.png" ) );

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if( !base.b活性化してない )
            {
                CDTXMania.tテクスチャの解放( ref this.txスピーカー );

                base.OnManagedリソースの解放();
            }
        }

        // CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {
            if( !base.b活性化してない )
            {
                int num = ( int )this.ctBPM.db現在の値;

                float num1 = 1.0698f - ( float )( Math.Sin( C変換.DegreeToRadian( num ) ) / 4.0f ); //2015.09.19 kairera0467 拍動を仮実装。

                float f位置X = 218;
                float f位置Y = 231;
                float f下差Y = 23;

                this.ctBPM.t進行LoopDb();

                if( this.txスピーカー != null && this.txスピーカー != null )
                {
                    if( CDTXMania.DTX.bチップがある.Guitar )
                    {
                        if( this.txスピーカー != null )
                        {
                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 255, 24, new Rectangle( 2, 202, 188, 11 ) );
                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 255, 223, new Rectangle( 2, 202, 188, 11 ) );

                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 255, 509, new Rectangle( 2, 202, 188, 11 ) );
                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 255, 708, new Rectangle( 2, 202, 188, 11 ) );
                        }

                        Matrix mat1u = Matrix.Identity;
                        mat1u *= Matrix.Scaling( num1, num1, 1f);
                        mat1u *= Matrix.Translation( 0f - f位置X, 0f + f位置Y, 0f );

                        this.txスピーカー.t3D描画( CDTXMania.app.Device, mat1u, new Rectangle( 2, 2, 188, 188 ) );

                        Matrix mat1d = Matrix.Identity;
                        mat1d *= Matrix.Scaling( num1, num1, 1f);
                        mat1d *= Matrix.Translation(0f - f位置X, 0f - f位置Y - f下差Y, 0f);

                        this.txスピーカー.t3D描画( CDTXMania.app.Device, mat1d, new Rectangle( 2, 2, 188, 188 ) );
                    }

                    if( CDTXMania.DTX.bチップがある.Bass )
                    {
                        if( this.txスピーカー != null )
                        {
                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 840, 24, new Rectangle( 2, 226, 188, 11 ) );
                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 840, 223, new Rectangle( 2, 226, 188, 11 ) );

                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 840, 509, new Rectangle( 2, 226, 188, 11 ) );
                            this.txスピーカー.t2D描画( CDTXMania.app.Device, 840, 708, new Rectangle( 2, 226, 188, 11 ) );
                        }

                        Matrix mat2u = Matrix.Identity;
                        mat2u *= Matrix.Scaling( num1, num1, 1f );
                        mat2u *= Matrix.Translation( 0f + f位置X, 0f + f位置Y, 0f );

                        this.txスピーカー.t3D描画( CDTXMania.app.Device, mat2u, new Rectangle( 2, 2, 188, 188 ) );

                        Matrix mat2d = Matrix.Identity;
                        mat2d *= Matrix.Scaling( num1, num1, 1f );
                        mat2d *= Matrix.Translation( 0f + f位置X, 0f - f位置Y - f下差Y, 0f );

                        this.txスピーカー.t3D描画( CDTXMania.app.Device, mat2d, new Rectangle( 2, 2, 188, 188 ) );

                    }
                }
            }
            return 0;
        }
    }
}
