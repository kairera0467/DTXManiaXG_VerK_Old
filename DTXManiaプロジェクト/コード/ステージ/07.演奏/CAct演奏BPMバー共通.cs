using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
	internal class CAct演奏BPMバー共通 : CActivity
	{
        /// <summary>
        /// BPMバーのクラス。BPMバーを置いてるだけ。
        /// 今回は下地と穴で分ける。
        /// (OFF時は穴を消せるようにして、置き換えの手間を省く。)
        /// 
        /// 2015.01.10 kairera0467 共通クラスにした。
        /// </summary>
        protected CTexture txバー下地;
        protected CTexture txバー穴;
        protected CTexture txバー本体;
        public CCounter ctBPMバー;
        public double UnitTime;

		// コンストラクタ

		public CAct演奏BPMバー共通()
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
			if( !base.b活性化してない )
			{
                this.txバー下地 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BarTops_base.png" ) );
                this.txバー穴 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BarTops.png" ) );
                this.txバー本体 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BarTops.png" ) );
                base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txバー下地 );
                CDTXMania.tテクスチャの解放( ref this.txバー穴 );
                CDTXMania.tテクスチャの解放( ref this.txバー本体 );
				base.OnManagedリソースの解放();
			}
		}

	}
}
