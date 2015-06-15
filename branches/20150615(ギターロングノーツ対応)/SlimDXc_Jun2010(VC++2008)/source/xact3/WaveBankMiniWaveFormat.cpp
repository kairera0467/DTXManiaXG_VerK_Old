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

#include "WaveBankMiniWaveFormat.h"

namespace SlimDX
{
namespace XACT3
{
	WaveBankMiniWaveFormat::WaveBankMiniWaveFormat(const WAVEBANKMINIWAVEFORMAT& format)
	{
		formatTag = static_cast<WaveBankMiniFormatTag>(format.wFormatTag);
		channels = format.nChannels;
		samplesPerSecond = format.nSamplesPerSec;

		if (formatTag == WaveBankMiniFormatTag::Wma)
		{
			blockAlign = format.wBlockAlign & 0x3F;
			bytesPerSecond = (format.wBlockAlign >> 6) & 0x03;
			isWmaPro = format.wBitsPerSample == 1;
		}
		else
		{
			blockAlign = format.wBlockAlign;
			bitsPerSample = format.wBitsPerSample == 0 ? 8 : 16;
		}
	}
}
}
