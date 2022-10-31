// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Components/SphereComponent.h"
#include <vector>
#include <fstream>
#include "GameFramework/GameUserSettings.h"
#include "Engine/Engine.h"
#include "EfficiencyMeasure.generated.h"

UCLASS()
class PBLM_API AEfficiencyMeasure : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AEfficiencyMeasure();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	virtual void EndPlay(EEndPlayReason::Type Reason) override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

private:
	void SaveDeltaTimesToFile();


	std::vector<float> deltaTimes;
	const int ticksCount = 1000;
};
