#include <iostream>
#include "Student.h"

double Add(double a, double b) {
	return a + b;
}

int main()
{
	std::cout << "Hello, World!\n";

	double x = 3.0;
	double y = 5.0;
	double result = Add(x, y);	//
	std::cout << x << "+" << y << "=" << result<<"\n";

	//添加class类 Student，Student.h，Student.cpp
	Student* pStu = new Student();	//指向Student实例的指针pStu
	pStu->SayHello();

	result = pStu->Add(x, y);	//函数Add以类Student成员的方式使用，成为方法，成员函数
	std::cout << x << "+" << y << "=" << result;

	return 0;
}