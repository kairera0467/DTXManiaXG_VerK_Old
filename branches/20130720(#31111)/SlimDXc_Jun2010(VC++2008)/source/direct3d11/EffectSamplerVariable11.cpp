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

#include "Direct3D11Exception.h"

#include "Enums11.h"
#include "SamplerDescription11.h"
#include "EffectSamplerVariable11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectSamplerVariable::EffectSamplerVariable( ID3DX11EffectSamplerVariable* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectSamplerVariable::EffectSamplerVariable( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectSamplerVariable*>( pointer.ToPointer() );
	}

	SamplerState^ EffectSamplerVariable::GetSamplerState(int index)
	{
		ID3D11SamplerState *result;

		HRESULT hr = m_Pointer->GetSampler(index, &result);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return SamplerState::FromPointer(result);
	}

	Result EffectSamplerVariable::SetSamplerState(int index, SamplerState^ blendState)
	{
		HRESULT hr = m_Pointer->SetSampler(index, blendState->InternalPointer);
		return RECORD_D3D11(hr);
	}

	SamplerDescription EffectSamplerVariable::GetBackingStore(int index)
	{
		D3D11_SAMPLER_DESC desc;

		HRESULT hr = m_Pointer->GetBackingStore(index, &desc);
		if (RECORD_D3D11(hr).IsFailure)
			return SamplerDescription();

		return SamplerDescription(desc);
	}

	Result EffectSamplerVariable::UndoSetSamplerState(int index)
	{
		HRESULT hr = m_Pointer->UndoSetSampler(index);
		return RECORD_D3D11(hr);
	}
}
}
