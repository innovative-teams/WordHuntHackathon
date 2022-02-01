import { WordModel } from './wordModel';

export interface VerifyTheWordRequestDto {
  word: WordModel;
  newWord: string;
}
