#include<stdio.h>
#include <iostream>

//函数指针类型
typedef int(*Calc)(int a, int b);

int Add(int a, int b)
{
	return a + b;
}

int Sub(int a, int b)
{
	return a - b;
}



int main()
{
	int x = 100;
	int y = 200;
	int z = 0;

	Calc FuncPoint1 = &Add;
	Calc FuncPoint2 = &Sub;
	/*z = Add(x,y);*/
	z = FuncPoint1(x,y);
	printf("%d+%d= %d\n",x,y,z);

	/*z = Sub(x, y);*/
	z = FuncPoint2(x, y);
	printf("%d-%d= %d\n", x, y, z);

	system("pause");
	return 0;
}