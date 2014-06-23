using UnityEngine;
using System.Collections;

public class Automata {
	public static int[,] CreateMap(int width, int height, int iterations, int killAmount, int growAmount, int doubleGrowAmount) {
		int size = width * height;

		int[,] cell = new int[width,height];
		int[,] cell2 = new int[width, height];
		
		//Fill randomly
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				cell[x,y] = Random.Range(0,2);
				cell2[x,y] = cell[x,y];
			}
		}
		
		//Apply rules for X iterations
		for (int i = 0; i < iterations; i++)
		{
			int[,] source = cell;
			int[,] dest = cell2;
			
			if (i % 2 != 0)
			{
				source = cell2;
				dest = cell;
			}
			
			for (int x = 0; x < width / 2; x++)
			{
				for (int y = 0; y < height; y++)
				{
					int currentVal = source[x, y];
					dest[x, y] = currentVal;
					
					int countNearMe = 0;

					for (int x1 = x - 1; x1 < x + 1; x1++)
					{
						for (int y1 = y - 1; y1 < y + 1; y1++)
						{
							if (x1 == x && y1 == y)
								continue;

							if (x1 < 0 || x1 >= width || y1 < 0 || y1 >= height)
								continue;

							countNearMe += source[x1, y1];
						}
					}

					//Try to kill
					if (countNearMe < killAmount && currentVal == 1)
						dest[x, y] = 0;
					
					//Try to grow
					if (countNearMe > growAmount && currentVal == 0)
						dest[x, y] = 1;
					
					//Try to grow upwards?
					if (countNearMe > doubleGrowAmount && currentVal == 1)
						dest[x, y] = 2;
				}
			}
		}
		
		if (iterations % 2 == 1)
			cell = cell2;
		
		for (int x = 0; x < width/2; x++)
		{
			for (int y = 0; y < height; y++)
			{
				cell[width - 1 - x, y] = cell[x, y];
			}
		}
		
		return cell;
	}

	public static int[,] CreateHexMap(int width, int height, int iterations, int killAmount, int growAmount, int doubleGrowAmount) {
		int size = width * height;
		
		int[,] cell = new int[width,height];
		int[,] cell2 = new int[width, height];
		
		//Fill randomly
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (y %2 == 1 && x == width - 1)
					continue;

				cell[x,y] = Random.Range(0,2);
				cell2[x,y] = cell[x,y];
			}
		}
		
		//Apply rules for X iterations
		for (int i = 0; i < iterations; i++)
		{
			int[,] source = cell;
			int[,] dest = cell2;
			
			if (i % 2 != 0)
			{
				source = cell2;
				dest = cell;
			}
			
			for (int x = 0; x < width / 2; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (y %2 == 1 && x == width - 1)
						continue;

					int currentVal = source[x, y];
					dest[x, y] = currentVal;
					
					int countNearMe = 0;

					countNearMe += Automata.valueForPosition(x, y - 2, width, height, source);
					countNearMe += Automata.valueForPosition(x, y - 1, width, height, source);
					countNearMe += Automata.valueForPosition(x, y + 1, width, height, source);
					countNearMe += Automata.valueForPosition(x, y + 2, width, height, source);
					countNearMe += Automata.valueForPosition(x - 1, y + 1, width, height, source);
					countNearMe += Automata.valueForPosition(x - 1, y - 1, width, height, source);
					
					//Try to kill
					if (countNearMe < killAmount && currentVal == 1)
						dest[x, y] = 0;
					
					//Try to grow
					if (countNearMe > growAmount && currentVal == 0)
						dest[x, y] = 1;
					
					//Try to grow upwards?
					if (countNearMe > doubleGrowAmount && currentVal == 1)
						dest[x, y] = 2;
				}
			}
		}
		
		if (iterations % 2 == 1)
			cell = cell2;
		
		for (int x = 0; x < width/2; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (y % 2 == 1)
					cell[width - 2 - x, y] = cell[x, y];
				else
					cell[width - 1 - x, y] = cell[x, y];
			}
		}
		
		return cell;
	}

	static int valueForPosition(int x, int y, int width, int height, int[,] map)
	{
		if (x < 0 || x >= width || y < 0 || y >= height)
			return 0;

		return map [x, y];
	}
}