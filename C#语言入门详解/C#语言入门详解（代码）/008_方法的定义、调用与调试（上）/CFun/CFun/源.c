#include <stdio.h>

double Add(double a, double b)	//Function fun
{
	return a + b;
}
int main() 
{
	printf("Hello, World!\n");	//CFun\x64\Debug\CFun.exe	按住Shift右击空白处在此处打开命令窗口，输入dir回车，输入cfun回车执行

	double x = 3.0;
	double y = 5.0;
	double result = Add(x, y);
	printf("%f+%f=%f", x, y, result);
	return 0;
}