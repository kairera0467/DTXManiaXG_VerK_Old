using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏DrumsコンボDGB : CAct演奏Combo共通
	{
		// CAct演奏Combo共通 実装
        public override void On活性化()
        {
            base.On活性化();
        }

        public void Start()
        {
            for (int j = 0; j < 1; j++)
            {
                if (this.st爆発[j].b使用中)
                {
                    this.st爆発[j].ct進行.t停止();
                    this.st爆発[j].b使用中 = false;
                }
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (!this.st爆発[j].b使用中)
                    {
                        this.st爆発[j].b使用中 = true;
                        this.st爆発[j].ct進行 = new CCounter(0, 13, 20, CDTXMania.Timer);
                        break;
                    }
                }
            }
        }

		protected override void tコンボ表示・ギター( int nCombo値, int nジャンプインデックス )
		{
			int x, y;
			if( CDTXMania.DTX.bチップがある.Bass )
			{
				x = 0x222;
				y = CDTXMania.ConfigIni.bReverse.Guitar ? 0xaf : 270;
				if( base.txCOMBOギター != null )
				{
					base.txCOMBOギター.n透明度 = 120;
				}
			}
			else
			{
				x = 0x1c0;
				y = CDTXMania.ConfigIni.bReverse.Guitar ? 0xee : 0xcf;
				if( base.txCOMBOギター != null )
				{
					base.txCOMBOギター.n透明度 = 0xff;
				}
			}
			base.tコンボ表示・ギター( nCombo値, x, y, nジャンプインデックス );
		}
		protected override void tコンボ表示・ドラム( int nCombo値, int nジャンプインデックス )
		{
			base.tコンボ表示・ドラム( nCombo値, nジャンプインデックス );

            if (nCombo値 % 100 == 0)
            {
                this.Start();
            }

            int x;
            int y = (CDTXMania.ConfigIni.bReverse.Drums ? 440 : -80 );
            switch (CDTXMania.ConfigIni.ドラムコンボ文字の表示位置)
            {
                case Eドラムコンボ文字の表示位置.LEFT:
                    x = -10;
                    break;
                case Eドラムコンボ文字の表示位置.CENTER:
                    x = 420;
                    break;
                case Eドラムコンボ文字の表示位置.RIGHT:
                    x = 950;
                    break;
                default:
                    x = 1300;
                    break;
            }
            if (nCombo値 >= 100)
            {
                for (int i = 0; i < 1; i++)
                {
                    if (this.st爆発[i].b使用中)
                    {
                        int num1 = this.st爆発[i].ct進行.n現在の値;
                        this.st爆発[i].ct進行.t進行();
                        if (this.st爆発[i].ct進行.b終了値に達した)
                        {
                            this.st爆発[i].ct進行.t停止();
                            this.st爆発[i].b使用中 = false;
                        }
                        if (this.txComboBom != null)
                        {
                            this.txComboBom.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, (340 * num1), 360, 340));
                        }
                    }
                }
            }

		}
		protected override void tコンボ表示・ベース( int nCombo値, int nジャンプインデックス )
		{
			int x = 0x1b5;
			int y = CDTXMania.ConfigIni.bReverse.Bass ? 0xaf : 270;
			if( base.txCOMBOギター != null )
			{
				base.txCOMBOギター.n透明度 = 120;
			}
			base.tコンボ表示・ベース( nCombo値, x, y, nジャンプインデックス );
		}
	}
}
