// Fill out your copyright notice in the Description page of Project Settings.


#include "LibBarBP.h"
#include "bar.h"

int ULibBarBP::LibBar_test()
{
	sqlite3* memorydb = nullptr;
	sqlite3_open(":memory:", &memorydb);
	sqlite3_exec(memorydb, "CREATE TABLE foo (id INTEGER, value TEXT)",	nullptr, nullptr, nullptr);
	sqlite3_close(memorydb);
	return 0;
}
