#include<stdio.h>

double getCylinderVolume(double r, double h) 
{
	double area = 3.1416 * r * r;
	double volume = area * h;
	return volume;
}

int main()
{
	double result = getCylinderVolume(10, 100);
	printf("Volume+%f\n", result);


	int x = 100;
	int y = 200; 
	x + y;	//C语言中可以编译，但没有意义
	x;
	y;
	100;

	return 0;
}