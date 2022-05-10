// Fill out your copyright notice in the Description page of Project Settings.


#include "LibFooBP.h"
#include "foo.hpp"

int ULibFooBP::LibFoo_Foo(int Value)
{
	return libfoo::foo(Value % 255);
}
