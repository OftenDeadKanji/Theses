// Fill out your copyright notice in the Description page of Project Settings.


#include "EfficiencyMeasure.h"

// Sets default values
AEfficiencyMeasure::AEfficiencyMeasure()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	RootComponent = CreateDefaultSubobject<USphereComponent>(TEXT("Root"));
}

// Called when the game starts or when spawned
void AEfficiencyMeasure::BeginPlay()
{
	Super::BeginPlay();
	
}

void AEfficiencyMeasure::EndPlay(EEndPlayReason::Type Reason)
{
	
}

// Called every frame
void AEfficiencyMeasure::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	deltaTimes.push_back(DeltaTime);
	if (deltaTimes.size() == ticksCount)
		SaveDeltaTimesToFile();
}

void AEfficiencyMeasure::SaveDeltaTimesToFile()
{
	std::ofstream file("RESULTS.txt", std::ios::out | std::ios::trunc);

	for (auto& time : deltaTimes)
	{
		file << time << '\n';
	}

	GetWorld()->GetFirstPlayerController()->ConsoleCommand("quit");
}

