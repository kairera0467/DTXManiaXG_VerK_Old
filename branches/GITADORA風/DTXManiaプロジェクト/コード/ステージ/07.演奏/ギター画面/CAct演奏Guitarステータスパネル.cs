using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txジャケットパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_JacketPanel.png"));
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if (!File.Exists(path))
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"));
                }
                else
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(path);
                }

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txジャケットパネル );
                CDTXMania.tテクスチャの解放( ref this.txジャケット画像 );
                base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                if ( this.txジャケットパネル != null )
                    this.txジャケットパネル.t2D描画(CDTXMania.app.Device, 467, 287);

                if (this.txジャケット画像 != null)
                {
                    SlimDX.Matrix mat = SlimDX.Matrix.Identity;
                    mat *= SlimDX.Matrix.Scaling(245.0f / this.txジャケット画像.sz画像サイズ.Width, 245.0f / this.txジャケット画像.sz画像サイズ.Height, 1f);
                    mat *= SlimDX.Matrix.Translation(-28f, -94.5f, 0f);
                    mat *= SlimDX.Matrix.RotationZ(0.3f);

                    this.txジャケット画像.t3D描画(CDTXMania.app.Device, mat);
                }

                //CDTXMania.act文字コンソール.tPrint(0, 100, C文字コンソール.Eフォント種別.白, string.Format("{0:####0}", CDTXMania.stage演奏ギター画面.bブーストボーナス ? 1 : 0));

			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//[StructLayout( LayoutKind.Sequential )]
		//private struct STATUSPANEL
		//{
		//    public string label;
		//    public int status;
		//}

		//private STATUSPANEL[] stパネルマップ;
        private CTexture txジャケットパネル;
        private CTexture txジャケット画像;
        //-----------------
		#endregion
	}
}
