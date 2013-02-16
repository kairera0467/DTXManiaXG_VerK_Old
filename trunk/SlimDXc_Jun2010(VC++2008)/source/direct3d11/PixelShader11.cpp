/*
* Copyright (c) 2007-2012 SlimDX Group
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
#include <d3d11.h>

#include "Direct3D11Exception.h"

#include "Device11.h"
#include "PixelShader11.h"
#include "ClassLinkage11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{
	PixelShader::PixelShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode )
	{
		ID3D11PixelShader *shader;

		HRESULT hr = device->InternalPointer->CreatePixelShader( shaderBytecode->InternalPointer->GetBufferPointer(), shaderBytecode->InternalPointer->GetBufferSize(), NULL, &shader );
		if( RECORD_D3D11( hr ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );

		Construct( shader );
	}

	PixelShader::PixelShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, ClassLinkage^ linkage )
	{
		ID3D11PixelShader *shader;
		ID3D11ClassLinkage *nativeLinkage = linkage == nullptr ? NULL : linkage->InternalPointer;

		HRESULT hr = device->InternalPointer->CreatePixelShader( shaderBytecode->InternalPointer->GetBufferPointer(), shaderBytecode->InternalPointer->GetBufferSize(), nativeLinkage, &shader );
		if( RECORD_D3D11( hr ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );

		Construct( shader );
	}
}
}
