using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
	internal class CActStageClear : CActivity
	{
		// CActivity 実装

		public override void On非活性化()
		{
            if (this.csStageClear != null)
            {
                this.csStageClear.t再生を停止する();
                CDTXMania.Sound管理.tサウンドを破棄する(this.csStageClear);
                this.csStageClear= null;
            }
		    base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの作成();
			}
		}
        
        
        public override void OnManagedリソースの解放()
        {
            if (this.b活性化してない)
                return;

            base.OnManagedリソースの解放();
        }

        
		public override int On進行描画()
		{
            if (!base.b活性化してない)
            {
                if (this.csStageClear != null)
                {
                    this.csStageClear.t再生を開始する();
                }
                this.t進行処理・サウンド();
            }
            return 0;
        }
        


		// その他

		#region [ private ]
		//-----------------
        private CSound csStageClear;

        private void tサウンドの作成()
		{
				try
				{
					this.csStageClear = CDTXMania.Sound管理.tサウンドを生成する( CSkin.Path(@"Sounds\stage clear.ogg") );
					this.csStageClear.n音量 = 100;
					this.csStageClear.t再生を開始する( true );
					Trace.TraceInformation( "サウンドを生成しました。({0})", CSkin.Path(@"Sounds\stage clear.ogg") );
				}
				catch
				{
					Trace.TraceError( "サウンドの生成に失敗しました。({0})", CSkin.Path(@"Sounds\stage clear.ogg") );
					if( this.csStageClear != null )
					{
						this.csStageClear.Dispose();
					}
					this.csStageClear = null;
				}
		}
		private void t進行処理・サウンド()
		{
			this.tサウンドの作成();
		}
        //-----------------
        #endregion
    }
}