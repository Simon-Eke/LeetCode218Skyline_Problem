namespace LeetCode218Skyline_Problem
{

    /*  output            correct
        2, 10             2, 10
        3, 15             3, 15
        5, 12             7, 12
        12, 0             12, 0
        15, 10            15, 10
        24, 0             20, 8
                          24, 0
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] buildings = [[2, 9, 10], [3, 7, 15], [5, 12, 12], [15, 20, 10], [19, 24, 8]];
            Solution solution = new();
            var output = solution.GetSkyline(buildings);
            foreach (var row in output)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }
    }

    public class Solution
    {
        public IList<IList<int>> GetSkyline(int[][] buildings)
        {
            IList<IList<int>> skyLine =
            [
                [buildings[0][0], buildings[0][2]]
            ];
            int maxLength = buildings[0][1];
            int newLongHeight = buildings[0][2]; // The max, several buildings can increase the local height. But if they aren't long enough the 
            int newLocalHeight = newLongHeight;
            if (buildings.Length > 1)
            {
                for (int i = 1; i < buildings.Length; i++)
                {
                    // If the next building doesn't overlap:    ____     _   if    _| |_     _
                    //                                         |    |___| |  if   |     |___| |
                    if (buildings[i][0] > maxLength)
                    {
                        if (newLocalHeight != newLongHeight) // If there are more buildings but we have a bump without a keypoint:
                        {
                            skyLine.Add([100, newLocalHeight]); // TODO - NOT 100 but has to be the previous 2nd highest local height.
                            newLongHeight = buildings[i][2]; 
                            newLocalHeight = newLongHeight;
                            // kör if satsen flera gånger om vi inte är nere på 0.
                        }
                        skyLine.Add([maxLength, 0]);
                        skyLine.Add([buildings[i][0], buildings[i][2]]);
                        maxLength = buildings[i][1];
                        continue;
                    }

                    // If the next building overlaps it could be either longer or shorter. max length should be updated if longer. Beware of doing it too soon though.
                    //     __|   |_    //      __|     |         &&        __|       |
                    // ___|        |__ //  ___|        |__       &&    ___|          |__

                    // new building increases maxLength
                    if (maxLength < buildings[i][1])
                    {
                        if (buildings[i][2] > newLocalHeight)
                            skyLine.Add([buildings[i][0], buildings[i][2]]); // TODO - repetition with row 63, break out or change.


                        maxLength = buildings[i][1];
                    }
                    else // doesn't increase, ie maxLength >= buildings[i][1]
                    {
                        if (buildings[i][2] > newLocalHeight)
                            skyLine.Add([buildings[i][0], buildings[i][2]]);
                    }
                        

                      /*Nästa byggnad överlappar och är högre. (1)
                            Men är kortare (2)
                        Nästa byggnad överlappar och är lägre. (3)
                            Och den gömda byggnaden där den överlappar men är lägre och faller inom
                            x1 < x2 && y2 < y1 och z2<z1
                      
                        TODO - If I create a pyramid starting with the bottom I will have to solve the back's steps "recursively"*/

                    // First try, assume that j in buildings[i][0] at least increase by 1.
                    
                }
            }
            skyLine.Add([maxLength, 0]);
            return skyLine;
        }
    }
}
