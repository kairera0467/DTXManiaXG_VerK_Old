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

#include "DirectWriteException.h"

#include "RenderingParameters.h"

const IID IID_IDWriteRenderingParams = __uuidof(IDWriteRenderingParams);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	float RenderingParameters::Gamma::get()
	{
		return InternalPointer->GetGamma();
	}
	float RenderingParameters::EnhancedContrast::get()
	{
		return InternalPointer->GetEnhancedContrast();
	}
	float RenderingParameters::ClearTypeLevel::get()
	{
		return InternalPointer->GetClearTypeLevel();
	}
	SlimDX::DirectWrite::PixelGeometry RenderingParameters::PixelGeometry::get()
	{
		return static_cast<SlimDX::DirectWrite::PixelGeometry>(InternalPointer->GetPixelGeometry());
	}
	SlimDX::DirectWrite::RenderingMode RenderingParameters::RenderingMode::get()
	{
		return static_cast<SlimDX::DirectWrite::RenderingMode>(InternalPointer->GetRenderingMode());
	}
}
}
