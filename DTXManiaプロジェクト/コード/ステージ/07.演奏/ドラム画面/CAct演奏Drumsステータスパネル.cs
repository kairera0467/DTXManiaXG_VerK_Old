using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
    {
        public override void On活性化()
        {
            base.On活性化();
        }
        public override void On非活性化()
        {
            CDTXMania.tテクスチャの解放( ref this.txジャケット画像 );
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if( !base.b活性化してない )
            {
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) )
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成( CSkin.Path(@"Graphics\5_preimage default.png" ) );
                }
                else
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成( path );
                }
                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if( !base.b活性化してない )
            {
                CDTXMania.tテクスチャの解放( ref this.txジャケット画像 );
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if( !base.b活性化してない )
            {
                //this.txジャケット画像.vc拡大縮小倍率.X = 245.0f / ((float)this.txジャケット画像.sz画像サイズ.Width);
                //this.txジャケット画像.vc拡大縮小倍率.Y = 245.0f / ((float)this.txジャケット画像.sz画像サイズ.Height);
                //this.txジャケット画像.fZ軸中心回転 = 0.3f;
                //this.txジャケット画像.t2D描画(CDTXMania.app.Device, 960, 350, new Rectangle(0, 0, this.txジャケット画像.sz画像サイズ.Width, this.txジャケット画像.sz画像サイズ.Height));
                Matrix mat = Matrix.Identity;
                mat *= Matrix.Scaling( 245.0f / this.txジャケット画像.sz画像サイズ.Width, 245.0f / this.txジャケット画像.sz画像サイズ.Height, 1f );
                mat *= Matrix.Translation( 440f, -335f, 0f );
                mat *= Matrix.RotationZ( 0.3f );

                this.txジャケット画像.t3D描画( CDTXMania.app.Device, mat );
            }
            return 0;

        }


        // その他

        #region [ private ]
        //-----------------
        private CTexture txジャケット画像;
        //-----------------
        #endregion
    }
}
