// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "UObject/Object.h"
#include "LibBarBP.generated.h"

/**
 * 
 */
UCLASS()
class LIBBAR_API ULibBarBP : public UObject
{
	GENERATED_BODY()

public:
	UFUNCTION(BlueprintCallable)
	int LibBar_test();
};
