// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "UObject/Object.h"
#include "LibFooBP.generated.h"

/**
 * 
 */
UCLASS()
class LIBFOO_API ULibFooBP : public UObject
{
	GENERATED_BODY()

public:
	UFUNCTION(BlueprintCallable)
	int LibFoo_Foo(int Value);
};
