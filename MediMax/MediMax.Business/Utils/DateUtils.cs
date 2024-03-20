namespace MediMax.Business.Utils
{
    public class DateUtils
    {
        public static bool HaveNoneConflicts(DateTime timeStart, DateTime timeEnd, DateTime timeStartConflictCandidate,
            DateTime timeEndConflictCandidate)
        {
            return
                   (timeStart < timeStartConflictCandidate || timeStart >= timeEndConflictCandidate) &&
                   (timeStart < timeStartConflictCandidate || timeEnd >= timeEndConflictCandidate) &&
                   (timeStart >= timeStartConflictCandidate || timeStart < timeEndConflictCandidate) &&
                   (timeStart >= timeStartConflictCandidate || timeEnd < timeEndConflictCandidate) &&
                   (timeEnd < timeStartConflictCandidate || timeStart >= timeEndConflictCandidate) &&
                   (timeEnd < timeStartConflictCandidate || timeEnd >= timeEndConflictCandidate) &&
                   (timeEnd >= timeStartConflictCandidate || timeStart < timeEndConflictCandidate) &&
                   (timeEnd >= timeStartConflictCandidate || timeEnd < timeEndConflictCandidate)
            ;
        }
    }
}