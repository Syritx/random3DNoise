using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenTK;

namespace DNoise
{
    public class Launcher
    {
        static PerlinNoise noise;

        static int[] dimensions = {
            40, 40, 120
        };

        static List<Vector3> heightMaps;
        static float amplitude = 30,
                     frequency = .7f,
                     minimum = -10f,
                     cubeSize = 5,
                     noiseScale = 20,
                     seedOffset = 0;

        static float seed = new Random().Next(1, 100000);
        static float yOffset = 0;

        public static void Main() {

            Console.WriteLine(seed);

            noise = new PerlinNoise();
            heightMaps = new List<Vector3>();
            heightMaps = CommitNoise();

            Task t = Task.Factory.StartNew(AnimateNoise);
            new Game(heightMaps, cubeSize, 1000, 1000);
            Task.WaitAll(t);
        }

        static void UpdateGame() {
            try
            {
                List<Cube> cubes = new List<Cube>();

                foreach (Vector3 v in heightMaps){
                    cubes.Add(new Cube(v, cubeSize));
                }
                Game.AnimateNoise(cubes, cubeSize);
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            return;
        }

        static void AnimateNoise() {

            while (true) {
                Thread.Sleep(10);
                seed += .1f;
                heightMaps = CommitNoise();
                UpdateGame();
            }
        }

        static List<Vector3> CommitNoise() {
            List<Vector3> heights = new List<Vector3>();

            for (int x = 0; x < dimensions[0]; x++) {
                for (int y = 0; y < dimensions[1]; y++) {
                    for (int z = 0; z < dimensions[2]; z++) {

                        float xNoise = 0,yNoise = 0,zNoise = 0;
                        float nx = (float)(x+yOffset)/noiseScale*frequency,
                              ny = (float)y/noiseScale*frequency,
                              nz = (float)z/noiseScale*frequency;

                        xNoise = (float)noise.noise(ny, nz, seed)*amplitude;
                        yNoise = (float)noise.noise(nx, nz, seed)*amplitude;
                        zNoise = (float)noise.noise(nx, ny, seed)*amplitude;

                        float density = xNoise + yNoise + zNoise;

                        if (density < minimum) {
                            //Console.WriteLine(ns);
                            heights.Add(new Vector3((x-(dimensions[0]/2))*cubeSize,(y-dimensions[1]) *cubeSize,(z-(dimensions[2]/ 2))*cubeSize));
                        }
                    }
                }
            }
            //yOffset++;
            return heights;
        } 
    }
}
