using System.Collections.Generic;
using UnityEngine;
/*
public class LapManager : MonoBehaviour
{
   public List<checkpoint> checkpoints;
   public UIManager manager;
   public int totalLaps = 3;
   private int lastPlayerCheckpoint = -1;
   private int currentPlayerLap = 0;

   void Start()
   {
       ListenCheckpoints(true);
   }

   private void ListenCheckpoints(bool subscribe)
   {
       foreach (checkpoint checkpoint in checkpoints)
       {
           if (subscribe) checkpoint.onCheckpointEnter.AddListener(CheckpointActivated);
           else checkpoint.onCheckpointEnter.RemoveListener(CheckpointActivated);
       }
   }

   public void CheckpointActivated(GameObject car, checkpoint checkpoint)
   {
       // Do we know this checkpoint ?
       if (checkpoints.Contains(checkpoint))
       {
           int checkpointNumber = checkpoints.IndexOf(checkpoint);
           // first time ever the car reach the first checkpoint
           bool startingFirstLap = checkpointNumber == 0 && lastPlayerCheckpoint == -1;
           // finish line checkpoint is triggered & last checkpoint was reached
           bool lapIsFinished = checkpointNumber == 0 && lastPlayerCheckpoint >= checkpoints.Count - 1;
           if (startingFirstLap || lapIsFinished)
           {
               currentPlayerLap += 1;
               lastPlayerCheckpoint = 0;
               manager.UpdateLapText("Lap " + currentPlayerLap + "/" + totalLaps);

               // if this was the final lap
               if (currentPlayerLap > totalLaps) { Debug.Log("You won"); manager.UpdateLapText("Finished!"); }
               else Debug.Log("Lap " + currentPlayerLap);
           }
           // next checkpoint reached
           else if (checkpointNumber == lastPlayerCheckpoint + 1) lastPlayerCheckpoint += 1;
       }
   }
}*/