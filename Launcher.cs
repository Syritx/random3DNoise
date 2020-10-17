using System;
using System.Collections.Generic;
using OpenTK;

namespace DNoise
{
    public class Launcher
    {
        static PerlinNoise noise;
        static int dimensions = 90;

        static List<Vector3> heightMaps;
        static float amplitude = 20,
                     minimum = 3f,
                     cubeSize = 5,
                     noiseScale = 30;

        static double seed = new Random().Next(1, 1000000);

        public static void Main() {

            noise = new PerlinNoise();
            heightMaps = new List<Vector3>();

            for (int x = 0; x < dimensions; x++) {
                for (int y = 0; y < dimensions; y++) {
                    for (int z = 0; z < dimensions; z++) {

                        float xNoise = 0,yNoise = 0,zNoise = 0;
                        float nx = (float)x/noiseScale,
                              ny = (float)y/noiseScale,
                              nz = (float)z/noiseScale;

                        xNoise = (float)noise.noise(ny, nz, seed)*amplitude;
                        yNoise = (float)noise.noise(nx, nz, seed)*amplitude;
                        zNoise = (float)noise.noise(nx, ny, seed)*amplitude;

                        float density = xNoise + yNoise + zNoise;

                        if (density > minimum) { 
                            //Console.WriteLine(ns);
                            heightMaps.Add(new Vector3((x-(dimensions/2))*cubeSize,(y-dimensions)*cubeSize,(z-(dimensions/2))*cubeSize));
                        }
                    }
                }
            }

            new Game(heightMaps, cubeSize, 1000, 1000);
        }
    }
}
