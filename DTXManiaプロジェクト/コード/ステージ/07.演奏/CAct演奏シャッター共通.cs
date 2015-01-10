using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏シャッター共通 : CActivity
	{
		// プロパティ

		protected CTexture txシャッター; //シャッター画像はギター・ドラムで別々。
        public STDGBVALUE<int> nシャッター座標;

		
		// コンストラクタ

		public CAct演奏シャッター共通()
		{
			base.b活性化してない = true;
		}

		// CActivity 実装

		public override void On活性化()
		{

			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
            //テクスチャの生成は子で行う
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.txシャッター );

				base.OnManagedリソースの解放();
			}
		}
	}
}
