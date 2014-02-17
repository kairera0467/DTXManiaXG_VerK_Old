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

        protected CTexture スピーカー上;
        protected CTexture スピーカー下;
        protected CTexture txスピーカー背景;
        public CCounter ctBPM;
        public double UnitTime;

        public override void On活性化()
        {
            this.ctBPM = null;
            this.UnitTime = 0.0;
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.スピーカー上 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_speakerU.png"));
                this.スピーカー下 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_speakerD.png"));
                this.txスピーカー背景 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_speaker.png"));

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放(ref this.スピーカー上);
                CDTXMania.tテクスチャの解放(ref this.スピーカー下);
                CDTXMania.tテクスチャの解放(ref this.txスピーカー背景);

                base.OnManagedリソースの解放();
            }
        }

        // CActivity 実装（共通クラスからの差分のみ）

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {
                int num = (int)this.ctBPM.db現在の値;

                float num1 = 1 + (float)( Math.Sin(Math.PI * num / 14));

                float f位置X = 218;
                float f位置Y = 231;
                float f下差Y = 23;

                if (this.スピーカー上 != null && this.スピーカー下 != null)
                {
                    if (CDTXMania.DTX.bチップがある.Guitar)
                    {
                        if (this.txスピーカー背景 != null)
                            this.txスピーカー背景.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 0, 640, 720));

                        Matrix mat1u = Matrix.Identity;
                        //mat1u *= Matrix.Scaling( num1, num1, 1f);
                        mat1u *= Matrix.Translation(0f - f位置X, 0f + f位置Y, 0f);

                        this.スピーカー上.t3D描画(CDTXMania.app.Device, mat1u);

                        Matrix mat1d = Matrix.Identity;
                        //mat1d *= Matrix.Scaling( num1, num1, 1f);
                        mat1d *= Matrix.Translation(0f - f位置X, 0f - f位置Y - f下差Y, 0f);

                        this.スピーカー下.t3D描画(CDTXMania.app.Device, mat1d);
                    }

                    if (CDTXMania.DTX.bチップがある.Bass)
                    {
                        if (this.txスピーカー背景 != null)
                            this.txスピーカー背景.t2D描画(CDTXMania.app.Device, 640, 0, new Rectangle(640, 0, 640, 720));

                        Matrix mat2u = Matrix.Identity;
                        //mat2u *= Matrix.Scaling(num1, num1, 1f);
                        mat2u *= Matrix.Translation(0f + f位置X, 0f + f位置Y, 0f);

                        this.スピーカー上.t3D描画(CDTXMania.app.Device, mat2u);

                        Matrix mat2d = Matrix.Identity;
                        //mat2d *= Matrix.Scaling(num1, num1, 1f);
                        mat2d *= Matrix.Translation(0f + f位置X, 0f - f位置Y - f下差Y, 0f);

                        this.スピーカー下.t3D描画(CDTXMania.app.Device, mat2d);

                    }
                }
            }
            return 0;
        }
    }
}
