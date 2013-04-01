using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				this.tx左パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_nameplate_Guitar.png" ) );
				this.tx右パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay status panels right.png" ) );
                this.tx曲名パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_songpanel.png"));
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx左パネル );
				CDTXMania.tテクスチャの解放( ref this.tx右パネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名パネル );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( this.tx左パネル != null )
				{
			        this.tx左パネル.t2D描画( CDTXMania.app.Device, 337, 211);
					int guitar = CDTXMania.ConfigIni.n譜面スクロール速度.Guitar;
					if( guitar < 0 )
					{
						guitar = 0;
					}
					if( guitar > 15 )
					{
						guitar = 15;
					}
				}
				if( this.tx右パネル != null )
				{
					//this.tx右パネル.t2D描画( CDTXMania.app.Device, 0x26e, 0x143, new Rectangle( this.nStatus * 15, 0xb7, 15, 0x49 ) );
					int bass = CDTXMania.ConfigIni.n譜面スクロール速度.Bass;
					if( bass < 0 )
					{
						bass = 0;
					}
					if( bass > 15 )
					{
						bass = 15;
					}
					//this.tx右パネル.t2D描画( CDTXMania.app.Device, 0x26e, 0x35, new Rectangle( bass * 15, 0, 15, 0xac ) );
				}
                this.tx曲名パネル.t2D描画(CDTXMania.app.Device, 515, 521);
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

		//private int nStatus;
		//private STATUSPANEL[] stパネルマップ;
		private CTexture tx右パネル;
		private CTexture tx左パネル;
        private CTexture tx曲名パネル;
		//-----------------
		#endregion
	}
}
