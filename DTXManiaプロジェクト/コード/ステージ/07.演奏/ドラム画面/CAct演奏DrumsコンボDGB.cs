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
            this.ctComboBom = new CCounter(0, 13, 20, CDTXMania.Timer);
            base.On活性化();
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
            this.ctComboBom.t進行();
			base.tコンボ表示・ドラム( nCombo値, nジャンプインデックス );

            if (nCombo値 % 100 == 0)
                this.ctComboBom.n現在の値 = 0;

            int num1 = this.ctComboBom.n現在の値;
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
                this.txComboBom.t2D描画(CDTXMania.app.Device, x, y, new Rectangle(0, (340 * num1), 360, 340));
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
