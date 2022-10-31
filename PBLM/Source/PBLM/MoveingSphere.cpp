// Fill out your copyright notice in the Description page of Project Settings.


#include "MoveingSphere.h"

// Sets default values
AMoveingSphere::AMoveingSphere()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void AMoveingSphere::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AMoveingSphere::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

