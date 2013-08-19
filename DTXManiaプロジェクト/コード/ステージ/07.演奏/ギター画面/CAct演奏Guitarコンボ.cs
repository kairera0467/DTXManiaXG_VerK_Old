using System;
using System.Collections.Generic;
using System.Text;

namespace DTXMania
{
	internal class CAct演奏Guitarコンボ : CAct演奏Combo共通
	{
		// CAct演奏Combo共通 実装

		protected override void tコンボ表示・ギター( int nCombo値, int nジャンプインデックス )
		{
			int x = 480;
			int y = 150;
            //XGではリバース時のコンボ位置はそのまま。
			if( base.txCOMBOギター != null )
			{
				base.txCOMBOギター.n透明度 = 0xff;
			}
			base.tコンボ表示・ギター( nCombo値, x, y, nジャンプインデックス );
		}
		protected override void tコンボ表示・ドラム( int nCombo値, int nジャンプインデックス )
		{
		}
		protected override void tコンボ表示・ベース( int nCombo値, int nジャンプインデックス )
		{
			int x = 810;
			int y = 150;
			if( base.txCOMBOギター != null )
			{
				base.txCOMBOギター.n透明度 = 0xff;
			}
			base.tコンボ表示・ベース( nCombo値, x, y, nジャンプインデックス );
		}
	}
}
