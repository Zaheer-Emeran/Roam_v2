// using System.Collections.Generic;
// using UnityEngine;

// public class Combat_Manager : MonoBehaviour
// {
//     public static List<Fighter> Fighters = new List<Fighter>();

//     [SerializeField] private float detect_Range = 6f;
//     [SerializeField] private float tick_Time = 0.5f;

//     private float timer;

//     private void Update()
// {
//     timer += Time.deltaTime;
//     if (timer < tick_Time) return;
//     timer = 0;


//     foreach (Fighter f in Fighters)
//     {
//         // clear the flag when nobody is fighting this unit
//         if (f.Target == null && !Is_Targeted(f)) f.in_Combat = false;

//         // keep the current target while it is still alive
//         if (f.Target != null) continue;

//         f.Target = Find_Nearest_Enemy(f);

//         if (f.Target != null)
//         {
//             f.In_Combat = true;
//             f.Target.In_Combat = true;
//             Debug.Log(f.name + " -> targets " + f.Target.name + " (was free: " + !f.Target.In_Combat + ")");
//         }
//     }
// }

//     private Fighter Find_Nearest_Enemy(Fighter me)
//     {
//         Fighter best_Free = null;
//         Fighter best_Busy = null;
//         float free_Distance = detect_Range;
//         float busy_Distance = detect_Range;

//         foreach (Fighter other in Fighters)
//         {
//             if (other.Is_Zombie == me.Is_Zombie) continue;   // same side

//             float d = Vector3.Distance(me.transform.position, other.transform.position);

//             if (other.In_Combat == false)
//             {
//                 if (d < free_Distance) { free_Distance = d; best_Free = other; }
//             }
//             else
//             {
//                 if (d < busy_Distance) { busy_Distance = d; best_Busy = other; }
//             }
//         }

//         // prefer a free enemy, otherwise join a fight (2v1)
//         if (best_Free != null) return best_Free;
//         return best_Busy;
//     }
//     private bool Is_Targeted(Fighter me)
//     {
//         foreach (Fighter other in Fighters)
//         {
//             if (other.Target == me) return true;
//         }
//         return false;
//     }
// }