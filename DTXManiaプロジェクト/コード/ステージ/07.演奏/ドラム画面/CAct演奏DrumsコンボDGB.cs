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
            for( int i = 0; i < 256; i++ )
            {
                this.b爆発した[ i ] = false;
                base.bn00コンボに到達した[i].Drums = false;
            }
            base.nコンボカウント.Drums = 0;
            this.n火薬カウント = 0;
            base.On活性化();
        }

        public void Start( int nCombo値 )
        {
            this.n火薬カウント = nCombo値 / 100;

 
            for (int j = 0; j < 1; j++)
            {
                if (this.st爆発[j].b使用中)
                {
                    this.st爆発[j].ct進行.t停止();
                    this.st爆発[j].b使用中 = false;
                    this.b爆発した[ this.n火薬カウント ] = true;

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
            bool guitar = CDTXMania.DTX.bチップがある.Guitar;
            bool bass = CDTXMania.DTX.bチップがある.Bass;
            var e表示位置 = CDTXMania.ConfigIni.ドラムコンボ文字の表示位置;
            int nX中央位置px = 0;

            #region [ e表示位置 の調整 ]
            //-----------------
            if (CDTXMania.ConfigIni.bGuitar有効)
            {
                if (bass)
                {
                    // ベースがあるときは問答無用で LEFT 表示のみ。
                    e表示位置 = Eドラムコンボ文字の表示位置.LEFT;
                }
                else if (guitar && (e表示位置 == Eドラムコンボ文字の表示位置.RIGHT))
                {
                    // ベースがなくてもギターがあるなら、RIGHT は CENTER に強制変更。
                    e表示位置 = Eドラムコンボ文字の表示位置.CENTER;
                }
            }
            //-----------------
            #endregion

            #region [ コンボ位置 ]
            switch (e表示位置)
            {
                case Eドラムコンボ文字の表示位置.LEFT:
                    nX中央位置px = 150;
                    break;

                case Eドラムコンボ文字の表示位置.CENTER:
                    nX中央位置px = 580;
                    break;

                case Eドラムコンボ文字の表示位置.RIGHT:
                    nX中央位置px = 1130;
                    break;
            }
            int nY上辺位置px = CDTXMania.ConfigIni.bReverse.Drums ? 530 : 16;
            #endregion

            base.tコンボ表示・ドラム(nCombo値, nジャンプインデックス, nX中央位置px, nY上辺位置px);

            this.n火薬カウント = (nCombo値 / 100);

            //if (nCombo値 % 100 == 0)
            if(( nCombo値 > (nCombo値 / 100) + 100) && this.b爆発した[ n火薬カウント ] == false )
            {
                this.Start( nCombo値 );
            }

            int x = nX中央位置px - 180;
            int y = nY上辺位置px - 95;

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
                            this.bn00コンボに到達した[this.nコンボカウント.Drums].Drums = true;
                        }
                        if ( this.txComboBom != null && CDTXMania.ConfigIni.ドラムコンボ文字の表示位置 != Eドラムコンボ文字の表示位置.OFF )
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
