using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class JumpState : State
{
    private float jumpHeight=5f; // peak height
    public JumpState(EnemyAI o, StateMachine s): base(o,s){}
    public override void Enter() { owner.StartCoroutine(JumpRoutine()); }
    public override void Tick() {}
    private IEnumerator JumpRoutine()
    {
        // Start link
        if(owner.agent.currentOffMeshLinkData.valid)
        {
            var data=owner.agent.currentOffMeshLinkData;
            Vector3 start=owner.transform.position;
            Vector3 end=data.endPos;
            float duration=Vector3.Distance(start,end)/owner.agent.speed;
            float elapsed=0;
            // release link for manual movement
            owner.agent.updatePosition=false;
            while(elapsed<duration)
            {
                float t=elapsed/duration;
                // parabolic interpolation
                Vector3 pos=Vector3.Lerp(start,end,t)+Vector3.up*jumpHeight*Mathf.Sin(Mathf.PI*t);
                owner.transform.position=pos;
                elapsed+=Time.deltaTime;
                yield return null;
            }
            owner.transform.position=end;
            owner.agent.CompleteOffMeshLink();
            owner.agent.updatePosition=true;
        }
        stateMachine.ChangeState(new ChaseState(owner,stateMachine));
        yield break;
    }
}
