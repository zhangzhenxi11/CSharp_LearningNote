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

	//���class�� Student��Student.h��Student.cpp
	Student* pStu = new Student();	//ָ��Studentʵ����ָ��pStu
	pStu->SayHello();

	result = pStu->Add(x, y);	//����Add����Student��Ա�ķ�ʽʹ�ã���Ϊ��������Ա����
	std::cout << x << "+" << y << "=" << result;

	return 0;
}