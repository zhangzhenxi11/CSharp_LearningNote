#include <stdio.h>

typedef int(*Calc)(int a, int b);	//声明函数指针

int Add(int a, int b)
{
	int result = a + b;
	return result;
}

int Sub(int a, int b)
{
	int result = a - b;
	return result;
}

int main()
{
	int x = 100;
	int y = 200;
	int z = 0;

	Calc funcPoint1 = &Add;
	Calc funcPoint2 = &Sub;

	z = Add(x, y);	//直接调用
	z = funcPoint1(x, y);	//间接调用
	printf("%d+%d=%d\n", x, y, z);

	z = Sub(x, y);
	z = funcPoint2(x, y);
	printf("%d-%d=%d\n", x, y, z);

	system("pause");
	return 0;
}