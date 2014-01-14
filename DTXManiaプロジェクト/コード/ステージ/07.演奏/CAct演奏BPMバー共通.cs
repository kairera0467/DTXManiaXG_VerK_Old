using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FDK;

namespace DTXMania
{
	internal class CAct演奏BPMバー共通 : CActivity
	{
		// プロパティ

        protected CTexture txBPMバー左;
        protected CTexture txBPMバー右;
        protected CTexture txBPMバーフラッシュ左;
        protected CTexture txBPMバーフラッシュ右;
        public CCounter ctBPMバー;
        public double UnitTime;


		// コンストラクタ

		public CAct演奏BPMバー共通()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド

		// CActivity 実装

		public override void On活性化()
		{
			base.On活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                string pathBPMFL = CSkin.Path(@"Graphics\7_BPMbar_Flush_L.png");
                string pathBPMFR = CSkin.Path(@"Graphics\7_BPMbar_Flush_R.png");
                string pathBPMbarL = CSkin.Path(@"Graphics\7_BPMbarL.png");
                string pathBPMbarR = CSkin.Path(@"Graphics\7_BPMbarR.png");
                if(File.Exists(pathBPMbarL) && File.Exists(pathBPMbarR))
                {
                    this.txBPMバー左 = CDTXMania.tテクスチャの生成(pathBPMbarL);
                    this.txBPMバー右 = CDTXMania.tテクスチャの生成(pathBPMbarR);
                }
                if(File.Exists(pathBPMFL) && File.Exists(pathBPMFR))
                {
                    this.txBPMバーフラッシュ左 = CDTXMania.tテクスチャの生成(pathBPMFL);
                    this.txBPMバーフラッシュ右 = CDTXMania.tテクスチャの生成(pathBPMFR);
                }

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放(ref this.txBPMバー左);
                CDTXMania.tテクスチャの解放(ref this.txBPMバー右);
                CDTXMania.tテクスチャの解放(ref this.txBPMバーフラッシュ左);
                CDTXMania.tテクスチャの解放(ref this.txBPMバーフラッシュ右);

				base.OnManagedリソースの解放();
			}
		}
	}
}
