import { createSelector } from 'reselect';

export interface SeriesQueueDetails {
  count: number;
  episodesWithFiles: number;
}

function createSeriesQueueDetailsSelector(
  seriesId: number,
  seasonNumber?: number
) {
  return createSelector(
    (state) => state.queue.details.items,
    (queueItems) => {
      return queueItems.reduce(
        (acc: SeriesQueueDetails, item) => {
          if (item.seriesId !== seriesId) {
            return acc;
          }

          if (seasonNumber != null && item.seasonNumber !== seasonNumber) {
            return acc;
          }

          acc.count++;

          if (item.episodeHasFile) {
            acc.episodesWithFiles++;
          }

          return acc;
        },
        {
          count: 0,
          episodesWithFiles: 0,
        }
      );
    }
  );
}

export default createSeriesQueueDetailsSelector;
