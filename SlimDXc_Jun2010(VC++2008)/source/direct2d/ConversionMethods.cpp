/*
* Copyright (c) 2007-2010 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#include "stdafx.h"

#include <d2d1.h>
#include <d2d1helper.h>

#include "ConversionMethods.h"

using namespace System;

D2D1_RECT_U ConvertRectangle( System::Drawing::Rectangle source )
{
	D2D1_RECT_U dest;
	dest.left = source.Left;
	dest.right = source.Right;
	dest.top = source.Top;
	dest.bottom = source.Bottom;

	return dest;
}

D2D1_RECT_F ConvertRectangle( System::Drawing::RectangleF source )
{
	D2D1_RECT_F dest;
	dest.left = source.Left;
	dest.right = source.Right;
	dest.top = source.Top;
	dest.bottom = source.Bottom;

	return dest;
}

System::Drawing::RectangleF CastRectangle( System::Drawing::Rectangle source )
{
	return System::Drawing::RectangleF( static_cast<FLOAT>( source.X ), static_cast<FLOAT>( source.Y ),
		static_cast<FLOAT>( source.Width ), static_cast<FLOAT>( source.Height ) );
}

System::Drawing::PointF CastPoint( System::Drawing::Point source )
{
	return System::Drawing::PointF( static_cast<FLOAT>( source.X ), static_cast<FLOAT>( source.Y ) );
}