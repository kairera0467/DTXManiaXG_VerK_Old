using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
	internal class CAct演奏LivePoint共通 : CActivity
	{
		// プロパティ

        public STDGBVALUE<int>[] x位置 = new STDGBVALUE<int>[10];
		public STDGBVALUE<double> n現在のLivePoint;
		protected CTexture txLivePoint;
        protected CTexture txLivePointゲージ;
        private CCounter[] ct箱 = new CCounter[15];

		
		// コンストラクタ

		public CAct演奏LivePoint共通()
		{
			base.b活性化してない = true;
		}

		public override void On活性化()
		{

			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txLivePointゲージ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_LivePointGauge.png" ) );
                this.txLivePoint = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\\7_LivePointGauge.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txLivePoint );
                CDTXMania.tテクスチャの解放( ref this.txLivePointゲージ );
				base.OnManagedリソースの解放();
			}
		}
	}
}
